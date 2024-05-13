﻿using Dapper;
using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.DatabaseAccess.Repositories;
using KursovaBack.Models;
using KursovaBack.Models.Enums;
using KursovaBack.Services.Interfaces;
using KursovaBack.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql;
using System.Data;
using static Dapper.SqlMapper;

namespace KursovaBack.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        IProjectRepository _projectRepository;
        string connectionString = "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=123456";
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public bool CreateProject(ProjectCreateViewModel project, Guid userId)
        {
            try
            {
                Guid projectId = Guid.NewGuid();
                project.CreatorId = userId;
                

                Project pr = new Project(projectId,project.Category,project.CreatorId,project.Name,project.Description,project.Analog,project.InvestmentAmount, project.InvestmentMoney);
                _projectRepository.Create(pr);
                using (IDbConnection db = new NpgsqlConnection(connectionString))
                {
                    try
                    {
                        int role = 1;
                        string sqlQuery = "Insert into project_user (user_id,project_id,project_role) values (@userId, @projectId, @role) ";
                        db.Execute(sqlQuery, new {userId,projectId,role});

                    }
                    catch (Exception ex)
                    {

                    }
                }
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}