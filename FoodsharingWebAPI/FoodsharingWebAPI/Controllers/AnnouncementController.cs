﻿using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : BaseController<Announcement>
    {
        public AnnouncementController(IRepository<Announcement> repository, ILogger<AnnouncementController> logger)
            : base(repository, logger) { }
    }
}
