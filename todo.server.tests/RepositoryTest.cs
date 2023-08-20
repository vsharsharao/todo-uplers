using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using todo.server.Data;
using todo.server.Data.Entities;
using todo.server.Data.Interfaces;
using Xunit;

namespace todo.server.tests
{
    public class RepositoryTest : IClassFixture<DataContextFixture>
    {
        private readonly DataContextFixture _fixture;
        private readonly IRepository<TodoTaskData> _repository;

        public RepositoryTest(DataContextFixture fixture)
        {
            _fixture = fixture;
            _repository = new Repository<TodoTaskData>(_fixture.DataContext);
        }

        [Fact]
        public void Get_ReturnsData()
        {
            var tasks = _repository.Get();

            // Assert
            Assert.NotNull(tasks);
            Assert.IsAssignableFrom<IEnumerable<TodoTaskData>>(tasks);
            Assert.True(tasks.Count() > 0);
        }

        [Fact]
        public void GetWithFilter_ReturnsData()
        {
            // Action
            var tasks = _repository.Get(i => i.Id == 1);

            // Assert
            Assert.NotNull(tasks);
            Assert.IsAssignableFrom<IEnumerable<TodoTaskData>>(tasks);
            Assert.Single(tasks);
        }

        [Fact]
        public async Task Create_PushesData()
        {
            // Action
            var task = await _repository.CreateAsync(new()
            {
                Title = "test_data_new",
                Description = "test_data_description"
            });

            // Assert
            Assert.NotNull(task);
            Assert.IsType<TodoTaskData>(task);
            var tasks = _repository.Get(i => i.Title == "test_data_new");
            Assert.Single(tasks);
        }

        [Fact]
        public async Task Update_UpdatesData()
        {
            var task = _repository.Get(i => i.Id == 1).First();
            task.Title = "test_data_updated";

            // Action
            await _repository.UpdateAsync(task);

            // Assert
            Assert.NotNull(task);
            Assert.IsType<TodoTaskData>(task);
            var updatedTask = _repository.Get(i => i.Id == 1).First();
            Assert.Equal("test_data_updated", updatedTask.Title);
        }

        [Fact]
        public async Task UpdateRange_UpdatesData()
        {
            var task1 = _repository.Get(i => i.Id == 1).First();
            var task2 = _repository.Get(i => i.Id == 2).First();
            task1.Title = "test_data_updated_1";
            task2.Title = "test_data_updated_2";

            // Action
            await _repository.UpdateRangeAsync(new List<TodoTaskData>() { task1, task2});

            // Assert
            var updatedTask1 = _repository.Get(i => i.Id == 1).First();
            var updatedTask2 = _repository.Get(i => i.Id == 2).First();
            Assert.Equal("test_data_updated_1", updatedTask1.Title);
            Assert.Equal("test_data_updated_2", updatedTask2.Title);
        }

        [Fact]
        public async Task Delete_DeletesData()
        {
            var task = _repository.Get(i => i.Id == 3).First();

            // Action
            await _repository.DeleteAsync(task);

            // Assert
            Assert.False(_repository.Get(i => i.Id == 3).Any());
        }

        [Fact]
        public async Task DeleteRange_DeletesData()
        {
            var task1 = _repository.Get(i => i.Id == 4).First();
            var task2 = _repository.Get(i => i.Id == 5).First();

            // Action
            await _repository.DeleteRangeAsync(new List<TodoTaskData>() { task2, task1});

            // Assert
            Assert.False(_repository.Get(i => i.Id == 4 || i.Id == 5).Any());
        }
    }
}
