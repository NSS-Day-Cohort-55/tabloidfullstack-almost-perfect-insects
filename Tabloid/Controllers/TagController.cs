using Microsoft.AspNetCore.Mvc;
using System;
using Tabloid.Models;
using Tabloid.Repositories;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Tabloid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

       //constructor: special method or fn; named the same as the class
       //create an instance of a class with a constructor; store that instance with a variable
        public TagController(ITagRepository tagRepository)
        {
           _tagRepository = tagRepository;
        }

     //GET: api/tag controller

    [HttpGet]
        public List<Tag> Get()
        {
            return _tagRepository.GetAllTags();
        }
    }
    }
