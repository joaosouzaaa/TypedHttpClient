﻿using Microsoft.AspNetCore.Mvc;
using TypedHttpClient.API.DataTransferObjects.Cep;
using TypedHttpClient.API.Services;

namespace TypedHttpClient.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ViaCepController(ViaCepService viaCepService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ViaCepResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<ViaCepResponse?> GetZipCodeInfoAsync([FromQuery] string zipCode, CancellationToken cancellationToken) =>
        viaCepService.GetZipCodeInfoAsync(zipCode, cancellationToken);
}
