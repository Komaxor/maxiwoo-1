using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mxc.IBSDiscountCard.Application.User.Commands;
using Mxc.IBSDiscountCard.Application.User.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.WebApi.Attributes;
using Mxc.IBSDiscountCard.Application.User.Queries;

namespace Mxc.IBSDiscountCard.WebApi.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserQueries _userQueries;

        public UserController(IMediator mediator, IUserQueries userQueries)
        {
            _mediator = mediator;
            _userQueries = userQueries;
        }

        /// <summary>
        /// Get profile data
        /// </summary>
        /// <returns>User profile data</returns>
        [HttpGet]
        public Task<UserDetailsViewModel> GetMyProfileAsync()
        {
            return _userQueries.GetMyProfileAsync();
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="command">User data</param>
        [AllowAnonymous]
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegisterUserAsync(RegisterUser command)
        {
            var response = await _mediator.Send(command);

            if (response.IsSuccess)
            {
                return Ok();
            }

            throw response.ExecutionError.ToApiErrorException();
        }

        /// <summary>
        /// Login with username, password
        /// </summary>
        /// <param name="command">Login data</param>
        /// <returns>JWT token for authorized calls</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginUserResponse>> LoginUserAsync(LoginUser command)
        {
            var response = await _mediator.Send(command);

            if (response.IsSuccess)
            {
                return response.Result;
            }

            throw response.ExecutionError.ToApiErrorException();
        }

        /// <summary>
        /// Resend activation code for user
        /// </summary>
        /// <param name="command">Empty object</param>
        [HttpPost]
        [Route("sendactivationcode")]
        public async Task<ActionResult<object>> SendActivationCodeAsync(SendActivationCode command)
        {
            var response = await _mediator.Send(command);

            if (response.IsSuccess)
            {
                return response.Result;
            }

            throw response.ExecutionError.ToApiErrorException();
        }

        /// <summary>
        /// Activate user with code
        /// </summary>
        /// <param name="command">Activation code</param>
        [HttpPost]
        [Route("activate")]
        public async Task<ActionResult<object>> ActivateUserAsync(ActivateUser command)
        {
            var response = await _mediator.Send(command);

            if (response.IsSuccess)
            {
                return response.Result;
            }

            throw response.ExecutionError.ToApiErrorException();
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="command">Password data</param>
        [HttpPost]
        [Route("changepassword")]
        public async Task<ActionResult<object>> ChangeMyPasswordAsync(ChangeMyPassword command)
        {
            var response = await _mediator.Send(command);

            if (response.IsSuccess)
            {
                return response.Result;
            }

            throw response.ExecutionError.ToApiErrorException();
        }

        /// <summary>
        /// Send password reset code
        /// </summary>
        /// <param name="command">Login email</param>
        [AllowAnonymous]
        [HttpPost]
        [Route("sendpasswordresetcode")]
        public async Task<ActionResult<object>> SendPasswordResetCodeAsync(SendPasswordResetCode command)
        {
            var response = await _mediator.Send(command);

            if (response.IsSuccess)
            {
                return response.Result;
            }

            throw response.ExecutionError.ToApiErrorException();
        }

        /// <summary>
        /// Set new password
        /// </summary>
        /// <param name="command">Code and new password</param>
        [AllowAnonymous]
        [HttpPost]
        [Route("setnewpassword")]
        public async Task<ActionResult<object>> SetNewPasswordAsync(SetNewPassword command)
        {
            var response = await _mediator.Send(command);

            if (response.IsSuccess)
            {
                return response.Result;
            }

            throw response.ExecutionError.ToApiErrorException();
        }
        
        /// <summary>
        /// Generate available payment options for the user
        /// </summary>
        /// <returns>Payment options</returns>
        [HttpPost]
        [Route("generate-payment-options")]
        public async Task<ActionResult<GeneratePaymentOptionsResponse>> GeneratePaymentOptionsAsync()
        {
            var response = await _mediator.Send(new GeneratePaymentOptions());

            if (response.IsSuccess)
            {
                return response.Result;
            }

            throw response.ExecutionError.ToApiErrorException();
        }

        /// <summary>
        /// Create a subscription for the user
        /// </summary>
        /// <param name="command">Payment provider specific data</param>
        [HttpPost]
        [Route("subscribe")]
        public async Task<ActionResult<object>> SubscribeAsync(Subscribe command)
        {
            var response = await _mediator.Send(command);

            if (response.IsSuccess)
            {
                return response.Result;
            }

            throw response.ExecutionError.ToApiErrorException();
        }

        /// <summary>
        /// Unsubscribe
        /// </summary>
        /// <param name="command">Email and password of the logged in user</param>
        [HttpPost]
        [Route("unsubscribe")]
        public async Task<ActionResult<object>> UnsubscribeAsync(Unsubscribe command)
        {
            var response = await _mediator.Send(command);

            if (response.IsSuccess)
            {
                return response.Result;
            }

            throw response.ExecutionError.ToApiErrorException();
        }
    }
}
