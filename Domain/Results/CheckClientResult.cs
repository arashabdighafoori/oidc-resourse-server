using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Results;

public class CheckClientResult
{
    public Client Client { get; set; }

    /// <summary>
    /// The clinet is found in my Clients Store
    /// </summary>
    public bool IsSuccess { get; set; }
    public string Error { get; set; }

    public string ErrorDescription { get; set; }
}
