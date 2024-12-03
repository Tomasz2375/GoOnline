using GoOnline.Domain.Entities;

namespace GoOnline.Api.Integration.Tests.Data;

public class ToDoIntegrationFaker : BaseIntegrationFaker<ToDo>
{
    public static IEnumerable<ToDo> GetAll()
    {
        return
        [
            new()
            {
                Id = BASE_ID + 1,
                Title = "TODO_TITLE_1",
                Description = "TODO_DESCRIPTION_1",
                Complete = 20.07m,
                ExpireDate = DateTime.Today,
            },
            new()
            {
                Id = BASE_ID + 2,
                Title = "TODO_TITLE_2",
                Description = "TODO_DESCRIPTION_2",
                ExpireDate = DateTime.Today.AddDays(1),
            },
            new()
            {
                Id = BASE_ID + 3,
                Title = "TODO_TITLE_3",
                Description = "TODO_DESCRIPTION_3",
                Complete = 80.70m,
                ExpireDate = DateTime.Today.AddDays(2),
            },
            new()
            {
                Id = BASE_ID + 4,
                Title = "TODO_TITLE_4",
                Description = "TODO_DESCRIPTION_4",
                Complete = 16.50m,
                ExpireDate = DateTime.Today.AddDays(10),
            },
            new()
            {
                Id = BASE_ID + 5,
                Title = "TODO_TITLE_5",
                Description = null,
                Complete = 50.32m,
                ExpireDate = DateTime.Today.AddDays(50),
            },
            new()
            {
                Id = BASE_ID + 6,
                Title = "TODO_TITLE_6",
                Description = "TODO_DESCRIPTION_6",
                Complete = 2.55m,
                ExpireDate = DateTime.Today.AddDays(400),
            },
        ];
    }
}
