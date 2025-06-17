namespace Shoghlana.Core.Interfaces;
public interface IUnitOfWork
{
    IFreelancerRepository freelancerRepository { get; }
   // IFreelancerNotificationRepository freelancerNotificationRepository { get; }
    IFreelancerSkillsRepository freelancerSkillsRepository { get; }

    ICategoryRepository categoryRepository { get; }

    IClientRepository clientRepository { get; }
    INotificationRepository NotificationRepository { get; }

    IJobRepository jobRepository { get; }
    IJobSkillsRepository jobSkillsRepository { get; }

    IProjectImagesRepository projectImagesRepository { get; }
    IProjectRepository projectRepository { get; }
    IProjectSkillsRepository projectSkillsRepository { get; }

    IProposalRepository proposalRepository { get; }
    IProposalImagesRepository proposalImageRepository { get; }

    IRateRepository rateRepository { get; }

    ISkillRepository skillRepository { get; }

    public IApplicationUserRepository ApplicationUserRepository { get; }


    //------------------------------------------------------

    public int Save();

    public Task<int> SaveAsync();

    void BeginTransaction();

    void Commit();

    void Rollback();
}
