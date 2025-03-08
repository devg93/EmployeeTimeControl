global using TimeInTimeOut.Module.Core.Domain.Entity;
global using Shared.Dto;
global using Shared.Services.ModuleCommunication.Contracts;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using System.Reflection;
global using Microsoft.Extensions.DependencyInjection;
global using TimeInTimeOut.Module.Core.Abstractions;
global using TimeInTimeOut.Module.Core.DAL;
global using TimeInTimeOut.Module.Core.Repository;
global using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.BackgroundService;
global using MediatR;

global using TimeInTimeOut.Module.Core.Dto;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.Extensions.Logging;
global using Shared.Services.Tasks.PingCheker;
global using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService;
global using Shared.Records;
global using Shared.Services.RunTime;
global  using Shared.Services.Tasks.ShedulerTuplelog;
global using Shared.Services.Tasks.ShedulerTuplelog.Enum;
global using TimeInTimeOut.Module.Core.DAL.Mediatr.Commands;

global using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.DAL.Mediatr.Commands;
global using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.DAL.Mediatr.Queries;
