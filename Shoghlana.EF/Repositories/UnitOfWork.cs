using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF.Repositories;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;

    private IDbContextTransaction _transaction;

    public ICategoryRepository category { get; private set; }

    public IClientRepository client { get; private set; }

    public IFreelancerRepository freelancer { get; private set; }

    public IJobRepository job { get; }

    public IJobSkillsRepository jobSkills { get; private set; }

    public IProjectImagesRepository projectImages { get; private set; }

    public IProjectRepository project { get; private set; }

    public IProjectSkillsRepository projectSkills { get; private set; }

    public IProposalRepository proposal { get; private set; }

    public IPropsalImageRepository ProposalImages { get; private set; }

    public IRateRepository rate { get; private set; }

    public ISkillRepository skill { get; }

    public IClientNotificationRepository clientNotification { get; private set; }

    public IFreelancerNotificationRepository freelancerNotification { get; private set; }

    public IFreelancerSkillsRepository freelancerSkills { get; private set; }
    public UnitOfWork(ApplicationDbContext context, ICategoryRepository categoryRepository, IClientRepository clientRepository,
               IFreelancerRepository freelancerRepository, IJobRepository jobRepository, IProjectImagesRepository projectImagesRepository,
               IProjectRepository projectRepository, IProjectSkillsRepository projectSkillsRepository, IRateRepository rateRepository,
               ISkillRepository skillRepository, IProposalRepository proposalRepository, IJobSkillsRepository jobSkillsRepository,
               IClientNotificationRepository clientNotificationRepository, IFreelancerNotificationRepository freelancerNotificationRepository
               , IPropsalImageRepository proposalImageRepository, IFreelancerSkillsRepository freelancerSkillsRepository)
    {
        _context = context;

        category = categoryRepository;
        client = clientRepository;
        freelancer = freelancerRepository;

        job = jobRepository;
        jobSkills = jobSkillsRepository;

        project = projectRepository;
        projectImages = projectImagesRepository;
        projectSkills = projectSkillsRepository;

        proposal = proposalRepository;
        ProposalImages = proposalImageRepository;


        rate = rateRepository;

        skill = skillRepository;

        freelancerSkills = freelancerSkillsRepository;

        clientNotification = clientNotificationRepository;
        freelancerNotification = freelancerNotificationRepository;
    }
    public int Save()
    {
        return _context.SaveChanges(); // num of affected entities in db
    }

    // as destructor >> called automatic when this request connection ends "if registered using addscoped" >>
    //release unmanaged resources like connection with db "like garbage collector but for unmanaged resources"
    public void Dispose()
    {
        _context.Dispose();
    }

    public void BeginTransaction()
    {
        if (_transaction == null)
        {
            _transaction = _context.Database.BeginTransaction();
        }
    }

    public void Commit()
    {
        try
        {
            _context.SaveChanges();
            _transaction?.Commit();
        }
        catch
        {
            Rollback();
            throw; // Re-throw exception to propagate
        }
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction = null;
    }
}
