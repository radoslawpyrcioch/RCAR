using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCAR.Api.DTOs.PaymentRecordDTOs;
using RCAR.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PaymentRecordController : ControllerBase
    {
        public readonly IPaymentRecordService _paymentRecordService;
        public PaymentRecordController(IPaymentRecordService paymentRecordService)
        {
            _paymentRecordService = paymentRecordService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(PaymentCreateDTO dto, int id)
        {
            var currentUserId = "Administrator@poczta.pl";
            if (currentUserId != null)
            {
                var resultDto = await _paymentRecordService.CreatePaymentAsync(dto, id, currentUserId);
                if (resultDto != false)
                {
                    return Created(string.Format("/PaymentRecord/{0}", dto.PaymentRecordId), resultDto);
                }
            }
            return BadRequest(new UnauthorizedAccessException());
        }
    }
}
