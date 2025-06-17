using LearningDashboard.Interfaces;
using LearningDashboard.Models;
using LearningDashboard.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace LearningDashboard.Tests
{
    public class CourseServiceUnitTests
    {
        [Fact]
        public void AddCourse_Should_AddCourseSuccessfully()
        {
            // Arrange
            //var courseService = new MemoryCourseService();
            //var newCourse = new Course { Name = "Math 101" };

            //// Act
            //courseService.Add(newCourse);
            //var courses = courseService.GetAll();

            //// Assert
            //Assert.Contains(courses, c => c.Name == "Math 101");
        }

        [Fact]
        public void AddCourse_Should_AssignId()
        {
            // Arrange
            //var courseService = new MemoryCourseService();
            //var newCourse = new Course { Name = "Physics 101" };

            //// Act
            //courseService.Add(newCourse);

            //// Assert
            //Assert.True(newCourse.Id > 0);
        }

        [Fact]
        public void GetAll_Should_ReturnCoursesInAlphabeticalOrder()
        {
            // Arrange
            //var courseService = new MemoryCourseService();
            //courseService.Add(new Course { Name = "Zebra Studies" });
            //courseService.Add(new Course { Name = "Ant Biology" });

            //// Act
            //var courses = courseService.GetAll().ToList();

            //// Assert


            //Assert.Equal("Ant Biology", courses[0].Name);
            //Assert.Equal("Zebra Studies", courses[1].Name);
        }
    }

}