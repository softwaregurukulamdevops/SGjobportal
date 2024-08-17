using JobPortel.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : Controller
    {
        public readonly TrainingDBContext trainingDBContext;
        public JobController(TrainingDBContext _trainingDBContext)
        {
            trainingDBContext = _trainingDBContext;
        }
        [HttpGet("GetJobDetails")]
        public List<Job> GetJobDetails()
        {
            List<Job> lstJob = new List<Job>();
            try
            {
                lstJob = trainingDBContext.Job.ToList();
                return lstJob;
            }
            catch (Exception ex)
            {
                lstJob = new List<Job>();
                return lstJob;
            }
        }
        [HttpPost("AddJob")]
        public string AddJob(Job job)
        {
            string message = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(job.JobName) && job.JobNumber >0)
                {
                    trainingDBContext.Add(job);
                    trainingDBContext.SaveChanges();
                    message = "Job added successfully";
                }
                else
                    message = "JobName and JobNumber required.";

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }
    }
}
