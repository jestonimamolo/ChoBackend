using choapi.DAL;
using choapi.DTOs;
using choapi.Messages;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionDAL _transactionDAL;

        private readonly ILogger<CreditController> _logger;

        public TransactionController(ILogger<CreditController> logger, ITransactionDAL transactionDAL)
        {
            _logger = logger;
            _transactionDAL = transactionDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<TransactionResponse> Add(TransactionDTO request)
        {
            var response = new TransactionResponse();
            try
            {
                if (request.User_Id <= 0)
                {
                    response.Message = "Required User_Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var transaction = new Transaction
                {
                    User_Id = request.User_Id,
                    Transaction_Type = request.Transaction_Type,
                    Type = request.Type,
                    Status = request.Status,
                    Created_Date = request.Created_Date
                };

                var result = _transactionDAL.Add(transaction);

                response.Transaction = result;
                response.Message = "Successfully added.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";
                return BadRequest(response);
            }
        }

        [HttpPost("update"), Authorize()]
        public ActionResult<TransactionResponse> Update(TransactionDTO request)
        {
            var response = new TransactionResponse();
            try
            {
                var transaction = _transactionDAL.Get(request.Transaction_Id);

                if (transaction != null)
                {
                    transaction.User_Id = request.User_Id;
                    transaction.Transaction_Type = request.Transaction_Type;
                    transaction.Type = request.Type;
                    transaction.Status = request.Status;
                    transaction.Created_Date = request.Created_Date;

                    var result = _transactionDAL.Update(transaction);

                    response.Transaction = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No transaction found by id: {request.Transaction_Id}";
                    response.Status = "Failed";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";
                return BadRequest(response);
            }
        }

        [HttpPost("delete/{id}"), Authorize()]
        public ActionResult<TransactionResponse> Delete(int id)
        {
            var response = new TransactionResponse();
            try
            {
                var transaction = _transactionDAL.Get(id);

                if (transaction != null)
                {
                    transaction.Is_Deleted = true;

                    var result = _transactionDAL.Update(transaction);

                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No transaction found by id: {id}";
                    response.Status = "Failed";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpGet("{id}"), Authorize()]
        public ActionResult<TransactionResponse> GetTransaction(int id)
        {
            var response = new TransactionResponse();
            try
            {
                var result = _transactionDAL.Get(id);

                if (result != null)
                {
                    response.Transaction = result;
                    response.Message = "Successfully get Transaction.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No transaction found by id: {id}";
                    response.Status = "Failed";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpGet("transactions/{id}"), Authorize()]
        public ActionResult<TransactionsResponse> GetTransactionByUserId(int id)
        {
            var response = new TransactionsResponse();
            try
            {
                var result = _transactionDAL.GetByUserId(id);

                if (result != null && result.Count > 0)
                {
                    response.Transactions = result;
                    response.Message = "Successfully get Transactions.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Transaction found by user id: {id}";
                    response.Status = "Failed";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }
    }
}
