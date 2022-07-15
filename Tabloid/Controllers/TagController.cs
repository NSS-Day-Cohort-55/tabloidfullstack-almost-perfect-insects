using Microsoft.AspNetCore.Mvc;
using System;
using Tabloid.Models;
using Tabloid.Repositories;
using System.Collections.Generic;

namespace Tabloid.Controllers
{ 
 public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

       //constructor for tag controller
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
