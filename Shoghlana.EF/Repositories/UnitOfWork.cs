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

    public IFreelancerRepository freelancerRepository { get; private set; }
    public IFreelancerNotificationRepository freelancerNotificationRepository { get; private set; }
    public IFreelancerSkillsRepository freelancerSkillsRepository { get; private set; }

    public ICategoryRepository categoryRepository { get; private set; }
    public IClientRepository clientRepository { get; private set; }
    public IClientNotificationRepository clientNotificationRepository { get; private set; }

    public IProjectImagesRepository projectImagesRepository { get; private set; }
    public IProjectRepository projectRepository { get; private set; }
    public IProjectSkillsRepository projectSkillsRepository { get; private set; }
   
    public IProposalRepository proposalRepository { get; private set; }
    public IProposalImagesRepository proposalImageRepository { get; private set; }

    public ISkillRepository skillRepository { get; }
    public IRateRepository rateRepository { get; private set; }

    public IJobRepository jobRepository { get; }
    public IJobSkillsRepository jobSkillsRepository { get; private set; }

    public IDbContextTransaction _transaction { get; private set; }


    public UnitOfWork
        (ApplicationDbContext context, IFreelancerRepository freelancerRepository,
        IFreelancerSkillsRepository freelancerSkillsRepository, IFreelancerNotificationRepository freelancerNotificationRepository,
        IClientRepository clientRepository, IClientNotificationRepository clientNotificationRepository, ICategoryRepository categoryRepository,
        IProjectRepository projectRepository, IProjectImagesRepository projectImagesRepository, IJobRepository jobRepository,
        IProjectSkillsRepository projectSkillsRepository, IProposalRepository proposalRepository, IJobSkillsRepository jobSkillsRepository,
        IProposalImagesRepository proposalImageRepository, ISkillRepository skillRepository, IRateRepository rateRepository)
    {
        _context = context;
        this.freelancerRepository = freelancerRepository;
        this.freelancerSkillsRepository = freelancerSkillsRepository;
        this.freelancerNotificationRepository = freelancerNotificationRepository;
        this.clientRepository = clientRepository;
        this.clientNotificationRepository = clientNotificationRepository;
        this.categoryRepository = categoryRepository;
        this.projectRepository = projectRepository;
        this.projectImagesRepository = projectImagesRepository;
        this.jobRepository = jobRepository;
        this.projectSkillsRepository = projectSkillsRepository;
        this.proposalRepository = proposalRepository;
        this.jobSkillsRepository = jobSkillsRepository;
        this.proposalImageRepository = proposalImageRepository;
        this.skillRepository = skillRepository;
        this.rateRepository = rateRepository;
    }
    public int Save()
    {
        return _context.SaveChanges(); // num of affected entities in db
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
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
