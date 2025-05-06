using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MediCore.Models;
using MediCore.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebSockets;
using System.Net.WebSockets;
using System.Text;
using MediCore.DTOs.ChatDTOs;
using AutoMapper;

namespace MediCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ChatController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageInputDTO chatMessageInputDTO)
        {
            if (chatMessageInputDTO.DoctorId <= 0 || chatMessageInputDTO.PatientId <= 0 || string.IsNullOrEmpty(chatMessageInputDTO.Message))
            {
                return BadRequest("DoctorId, PatientId, and Message are required.");
            }
            var chatMessage = new ChatMessage
            {
                DoctorId = chatMessageInputDTO.DoctorId,
                PatientId = chatMessageInputDTO.PatientId,
                Message = chatMessageInputDTO.Message,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
   
            var chatMessageSendDTO = _mapper.Map<ChatMessageSendDTO>(chatMessage);

            return Ok(chatMessageSendDTO);
        }

        // WebSocket endpoint to establish a connection for real-time communication
        [HttpGet("start-chat/{doctorId}/{patientId}")]
        public async Task StartChat(int doctorId, int patientId)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await HandleWebSocketConnection(webSocket, doctorId, patientId);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task HandleWebSocketConnection(WebSocket webSocket, int doctorId, int patientId)
        {
            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                }
                else
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    var chatMessage = new ChatMessage
                    {
                        DoctorId = doctorId,
                        PatientId = patientId,
                        Message = message,
                        Timestamp = DateTime.UtcNow
                    };

                    _context.ChatMessages.Add(chatMessage);
                    await _context.SaveChangesAsync();
                    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), result.MessageType, result.EndOfMessage, CancellationToken.None);
                }
            }
        }
    }
}
