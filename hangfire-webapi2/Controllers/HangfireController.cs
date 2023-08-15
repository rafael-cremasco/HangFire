using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;

namespace hangfire_webapi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangfireController : ControllerBase
    {

        [HttpPost]
        [Route("[action]")]

        public IActionResult bemvindo()
        {

            var jobId = BackgroundJob.Enqueue(() => enviarEmail("teste"));

            return Ok($"Jobid: {jobId}. email enviado para o usuario");
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult desconto()
        {
            int tempoSegundo = 30;
            var jobId = BackgroundJob.Schedule(() => enviarEmail("teste"),TimeSpan.FromSeconds(tempoSegundo));

            return Ok($"Jobid: {jobId}. desconto enviado por email em {tempoSegundo} segundos");
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult Confirm()
        {
            int tempoSegundo = 30;
            var parentJobId = BackgroundJob.Schedule(() => enviarEmail("teste"), TimeSpan.FromSeconds(tempoSegundo));

            BackgroundJob.ContinueJobWith(parentJobId, () => Console.WriteLine("parabens voce se inscreveu"));

            return Ok("job Criado");
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Databaseupdate()
        {

            RecurringJob.AddOrUpdate(() => Console.WriteLine("bem vindo a aplicacao"), Cron.Minutely);
            return Ok("inicializando aplicacao");
    
        }


        public void enviarEmail(string text)
        {

            Console.WriteLine(text);
        }
    }
}
