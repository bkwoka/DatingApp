﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Data;
using DatingApp.Dtos;
using DatingApp.Helpers;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IDatingRepository _repository;
        private readonly IMapper _mapper;

        public MessagesController(IDatingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messageFromRepo = await _repository.GetMessage(id);

            if (messageFromRepo == null)
                return NotFound();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(int userId, [FromQuery] MessageParams messageParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            messageParams.UserId = userId;

            var messagesFromRepo = await _repository.GetMessagesForUser(messageParams);

            var messages = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);
            
            Response.AddPagination(messagesFromRepo.CurrentPage,
                messagesFromRepo.PageSize,
                messagesFromRepo.TotalCount,
                messagesFromRepo.TotalPages);

            return Ok(messages);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messageFromRepo = await _repository.GetMessageThread(userId, recipientId);

            var messageThread = _mapper.Map<IEnumerable<MessageToReturnDto>>(messageFromRepo);
            
            return Ok(messageThread);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId,[FromBody] MessageForCreationDto messageForCreationDto)
        {

            var sender = await _repository.GetUser(userId);
            
            if (sender.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            messageForCreationDto.SenderId = userId;
            var recipient = await _repository.GetUser(messageForCreationDto.RecipientId);

            if (recipient == null)
                return BadRequest("Could not find user");

            var message = _mapper.Map<Message>(messageForCreationDto);
            
            _repository.Add(message);
            
            if (await _repository.SaveAll())
            {
                var messageToReturn = _mapper.Map<MessageToReturnDto>(message);
                return Created("", messageToReturn);
            }

            throw new Exception("Creating the message failed on save");
        }

        [HttpPost("{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messageFromRepo = await _repository.GetMessage(messageId);
            
            if (messageFromRepo.SenderId == userId)
                messageFromRepo.SenderDeleted = true;

            if (messageFromRepo.RecipientId == userId)
                messageFromRepo.RecipientDeleted = true;

            if (messageFromRepo.SenderDeleted && messageFromRepo.RecipientDeleted)
            {
                _repository.Delete(messageFromRepo);
            }

            if (await _repository.SaveAll())
                return NoContent();
            
            throw new Exception("Error deleting the message");
        }

        [HttpPost("{messageId}/read")]
        public async Task<IActionResult> MarkMessageAsRead(int userId, int messageId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var message = await _repository.GetMessage(messageId);

            if (message.RecipientId != userId)
                return Unauthorized();

            message.IsRead = true;
            message.DateRead = DateTime.Now;

            await _repository.SaveAll();
            
            return NoContent();
        }
    }
}