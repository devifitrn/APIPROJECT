using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BasesController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public ActionResult<Entity> Post(Entity entity)
        {
            /*repository.Insert(entity);
            return Ok(new { Status = HttpStatusCode.OK, result = entity, message = "Berhasil menambahkan data" });*/
            var result = repository.Insert(entity);
            if (result > 0)
            {
                return Ok(new { Status = HttpStatusCode.OK, result = entity, message = "Berhasil menambahkan data" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.BadRequest, result = entity, message = "Gagal menambahkan data" });
            }

        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            try
            {
                int count = repository.Get().ToList().Count;
                if (count > 0)
                {
                    return Ok(new { status = HttpStatusCode.OK, result = repository.Get(), message = "Data ditemukan" });
                }
                else
                {
                    return StatusCode(404, new { status = HttpStatusCode.NotFound, result = repository.Get(), message = "Data tidak ditemukan" });

                }
            }
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result = repository.Get(), message = "terjadi kesalahan" });
            }
        }

        [HttpGet("{Key}")]
        public ActionResult<Entity> Get(Key key)
        {
            try
            {
                if (repository.Get(key) == null)
                {
                    return StatusCode(404, new { status = HttpStatusCode.NotFound, result = repository.Get(key), message = "Data tidak ditemukan" });
                }
                else
                {

                    return Ok(new { status = HttpStatusCode.OK, result = repository.Get(key), message = "Data ditemukan" });
                }
            }
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result = repository.Get(key), message = "terjadi kesalahan" });
            }
        }

        [HttpDelete("{Key}")]
        public ActionResult<Entity> Delete(Key key)
        {
            try
            {
                repository.Delete(key);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException)
                {
                    return StatusCode(404, new { status = HttpStatusCode.NotFound, result = repository.Get(key), message = "Data tidak ada" });
                }
            }
            return Ok(new { status = HttpStatusCode.OK, result = repository.Get(key), message = "Data terhapus" });
        }

        [HttpPut]
        public ActionResult<Entity> UpdateData(Entity entity, Key key)
        {

            try
            {
               var result = repository.Update(entity, key);
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateConcurrencyException || ex is DbUpdateException)
                {
                    return StatusCode(404, new { status = HttpStatusCode.NotFound, result = "" , message = "Data tidak ada" });
                }
            }
            return Ok(new { status = HttpStatusCode.OK, result = "" , message = "Data terupdate" });

        }

    }

}
