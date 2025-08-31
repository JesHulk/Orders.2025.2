﻿using Orders.Backend.UnitsOfWork.Interfaces;

namespace Orders.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenericController<T>(IGenericUnitOfWork<T> unitOfWork) : ControllerBase where T : class
{
    private readonly IGenericUnitOfWork<T> _unitOfWork = unitOfWork;

    #region GET
    [HttpGet]
    public virtual async Task<IActionResult> GetAsync()
    {
        var action = await _unitOfWork.GetAsync();
        if (action.WasIsSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetAsync(int id)
    {
        var action = await _unitOfWork.GetAsync(id);
        if (action.WasIsSuccess)
        {
            return Ok(action.Result);
        }
        return NotFound();
    }
    #endregion

    #region POST
    [HttpPost]
    public virtual async Task<IActionResult> PostAsync(T model)
    {
        var action = await _unitOfWork.AddAsync(model);
        if (action.WasIsSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }
    #endregion

    #region PUT
    [HttpPut]
    public virtual async Task<IActionResult> PutAsync(T model)
    {
        var action = await _unitOfWork.UpdateAsync(model);
        if (action.WasIsSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }
    #endregion

    #region DELETE
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(int id)
    {
        var action = await _unitOfWork.DeleteAsync(id);
        if (action.WasIsSuccess)
        {
            return NoContent();
        }
        return BadRequest(action.Message);
    }
    #endregion
}
