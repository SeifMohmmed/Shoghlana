﻿using Shoghlana.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.Models;
public class Job
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime PostTime { get; set; } = DateTime.Now;

    public DateTime? ApproveTime { get; set; } // added manually when the client accept a proposal

    public string Description { get; set; }

    public decimal MinBudget { get; set; }

    public decimal MaxBudget { get; set; }

    public int DurationInDays { get; set; }


    private DateTime? _manualDeadLine;

    public DateTime? DeadLine
    {
        get => _manualDeadLine ?? ApproveTime?.AddDays(DurationInDays);
        set => _manualDeadLine = value;
    }

    /// <summary>
    ///  any time you access the ProposalsCount property,
    ///  it will always return the current count of the Proposals list,
    ///  you can also set it manually (added it in order not to breaka an existent code)
    ///  shorlty like this using Lampda expression : 
    /// </summary>

    private int? _manualProposalsCount = null;

    public int? ProposalsCount
    {
        get => _manualProposalsCount ?? Proposals?.Count() ?? 0;
        set => _manualProposalsCount = value;
    }

    public ExperienceLevel ExperienceLevel { get; set; }

    public List<JobSkills>? Skills { get; set; } = new List<JobSkills>();

    public List<Proposal>? Proposals { get; set; } = new List<Proposal>();

    public Rate? Rate { get; set; }

    public JobStatus Status { get; set; } = JobStatus.Active;

    //---------------------------------------------

    public int ClientId { get; set; }

    public Client Client { get; set; }

    public int? AcceptedFreelancerId { get; set; }

    public Freelancer AcceptedFreelancer { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }

}
