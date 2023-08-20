using Xunit;
using Moq;
using todo.server.Services.Interfaces;
using todo.server.Controllers;
using System.Collections.Generic;
using todo.server.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace todo.server.tests
{
    public class TodoControllerTest
    {
        private readonly Mock<ITodoService> _mockTodoService;
        private readonly TodoController _todoController;

        public TodoControllerTest()
        {
            _mockTodoService = new();
            _mockTodoService.Setup(i => i.GetTasks()).Returns(TestDataProvider.GetTasks());
            _mockTodoService.Setup(i => i.GetTaskById(1)).Returns(TestDataProvider.GetTasks().FirstOrDefault(i => i.Id == 1));
            _mockTodoService.Setup(i => i.CreateTaskAsync(It.IsAny<TodoTask>())).ReturnsAsync((TodoTask i) => i);
            _mockTodoService.Setup(i => i.UpdateTaskAsync(It.IsAny<TodoTask>())).ReturnsAsync((TodoTask i) => i);
            _mockTodoService.Setup(i => i.UpdateAsCompletedAsync(It.IsAny<long>(), It.IsAny<bool>())).ReturnsAsync((long id, bool isCompleted) => new() { IsCompleted = isCompleted});
            _mockTodoService.Setup(i => i.UpdateAsCompletedAsync(It.IsAny<IEnumerable<long>>())).ReturnsAsync((IEnumerable<long> ids) => Enumerable.Range(1, 3).Select(i => new TodoTask() { IsCompleted = true}));
            _mockTodoService.Setup(i => i.DeleteTaskAsync(It.IsAny<long>())).Verifiable();
            _mockTodoService.Setup(i => i.DeleteTaskAsync(It.IsAny<IEnumerable<long>>())).Verifiable();
            _todoController = new(_mockTodoService.Object);
        }

        [Fact]
        public void Get_ReturnsOk()
        {
            // Act
            var tasks = _todoController.Get();

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(tasks.Result);

            var result = tasks.Result as ObjectResult;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<TodoTask>>(result!.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Equal(3, ((IEnumerable<TodoTask>)result.Value!).Count());
        }

        [Fact]
        public void GetById_ReturnsOk()
        {
            // Arrange
            int taskId = 1;

            // Act
            var tasks = _todoController.Get(taskId);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(tasks.Result);

            var result = tasks.Result as ObjectResult;

            Assert.NotNull(result);
            Assert.IsType<TodoTask>(result!.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task Post_ReturnsCreated()
        {
            // Arrange
            TodoTask task = new("test_title", "test_description");

            // Act
            var createdTask = await _todoController.Post(task);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(createdTask.Result);

            var result = createdTask.Result as ObjectResult;

            Assert.NotNull(result);
            Assert.IsType<TodoTask>(result!.Value);
            Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Equal("test_title", ((TodoTask)result.Value!).Title);
        }

        [Fact]
        public async Task Put_ReturnsOk()
        {
            // Arrange
            TodoTask task = new("test_title", "test_description");

            // Act
            var updatedTask = await _todoController.Put(task);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(updatedTask.Result);

            var result = updatedTask.Result as ObjectResult;

            Assert.NotNull(result);
            Assert.IsType<TodoTask>(result!.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Equal("test_title", ((TodoTask)result.Value!).Title);
        }

        [Fact]
        public async Task Patch_ReturnsOk()
        {
            // Act
            var updatedTask = await _todoController.Patch(1, true);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(updatedTask.Result);

            var result = updatedTask.Result as ObjectResult;

            Assert.NotNull(result);
            Assert.IsType<TodoTask>(result!.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.True(((TodoTask)result.Value!).IsCompleted);
        }

        [Fact]
        public async Task PatchBulk_ReturnsOk()
        {
            // Act
            var updatedTask = await _todoController.Patch(new long[] { 1, 2, 3});

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(updatedTask.Result);

            var result = updatedTask.Result as ObjectResult;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<TodoTask>>(result!.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.All((IEnumerable<TodoTask>)result.Value!, i => Assert.True(i.IsCompleted));
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Act
            var deleteTask = await _todoController.Delete(1);

            // Assert
            Assert.IsAssignableFrom<OkResult>(deleteTask);

            var result = deleteTask as OkResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result!.StatusCode);
        }

        [Fact]
        public async Task DeleteBulk_ReturnsOk()
        {
            // Act
            var deleteTask = await _todoController.Delete(new long[] { 1, 2, 3});

            // Assert
            Assert.IsAssignableFrom<OkResult>(deleteTask);

            var result = deleteTask as OkResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result!.StatusCode);
        }
    }
}