﻿using Microsoft.AspNetCore.Mvc;
using TdM.Database.Models.Domain;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class CriaturasController : Controller
{
    private readonly ICriaturaRepository criaturaRepository;
    private readonly IMundoRepository mundoRepository;

    public CriaturasController(ICriaturaRepository criaturaRepository, IMundoRepository mundoRepository)
    {
        this.criaturaRepository = criaturaRepository;
        this.mundoRepository = mundoRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var criatura = await criaturaRepository.GetByUrlHandleAsync(urlHandle);
        return View(criatura);
    }

    [HttpGet]
    public async Task<IActionResult> List(string urlHandle)
    {
        if (urlHandle == null)
        {
            var criaturas = await criaturaRepository.GetAllAsync();
            var viewModel = new NavbarViewModel
            {
                Criaturas = criaturas
            };
            return View(viewModel);
        }
        else
        {
            var mundo = await mundoRepository.GetByUrlHandleAsync(urlHandle);
            var criaturas = await criaturaRepository.GetAllByMundoAsync(mundo.Id);
            ViewBag.MundoUrlHandle = mundo.UrlHandle; // set the value of ViewBag here
            var viewModel = new NavbarViewModel
            {
                Mundo = mundo,
                Criaturas = criaturas
            };
            return View(viewModel);
        }
    }
}
