using Arqom.Core.Contracts.ApplicationServices.Events;
using Microsoft.Extensions.Logging;
using MiniBlog.Core.Domain.Blogs.Events;
using MiniBlog.Core.Domain.People.Entities;
using MiniBlog.Core.Domain.People.Repositories;


namespace MiniBlog.Core.ApplicationService.Blogs.Events.BlogCreatedHandler;
public class BlogCreatedHandler : IDomainEventHandler<BlogCreated>
{
    private readonly ILogger<BlogCreatedHandler> _logger;
    private readonly IPersonCommandRepository _personCommandRepository;

    public BlogCreatedHandler(ILogger<BlogCreatedHandler> logger,
                                IPersonCommandRepository personCommandRepository)
    {
        _logger = logger;
        _personCommandRepository = personCommandRepository;
    }
    public async Task Handle(BlogCreated Event)
    {
        try
        {
            Person person = new Person
            {
                FirstName = DateTime.Now.ToString(),
                LastName = DateTime.Now.ToLongTimeString(),
            };
            await _personCommandRepository.InsertAsync(person);

            _logger.LogInformation("Handeled {Event} in BlogCreatedHandler", Event.GetType().Name);
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {

            throw;
        }

    }
}
