using System;
using System.Linq;
using Core.Interfaces;
using Core.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class TenantService : ITenantService
    {

        private readonly TenantSettings _tenantSettings;
        private HttpContext _httpContext;
        private Tenant _currentTenant;
        
        public TenantService(IOptions<TenantSettings> tenantSettings, IHttpContextAccessor contextAccessor)
        {
            _tenantSettings = tenantSettings.Value;
            _httpContext = contextAccessor.HttpContext;

            if (_httpContext != null)
            {
                if (_httpContext.Request.Headers.TryGetValue("X-TENANT", out var tenantId))
                {
                    SetTenant(tenantId);
                }
                else
                {
                    throw new Exception("Invalid TenantId");
                }
            }
        }

        private void SetTenant(string tenantId)
        {
            _currentTenant = _tenantSettings.Tenants.FirstOrDefault(a => a.TID == tenantId);
            if (_currentTenant == null) throw new Exception("Invalid Tenant");
            if (string.IsNullOrEmpty(_currentTenant.ConnectionString))
            {
                SetDefaultConnectionStringToCurrentTenant();
            }
        }

        private void SetDefaultConnectionStringToCurrentTenant()
        {
            _currentTenant.ConnectionString = _tenantSettings.Defaults.ConnectionString;
        }
        
        public string GetDatabaseProvider()
        {
            return _tenantSettings.Defaults?.DBProvider;
        }

        public string GetConnectionString()
        {
            return _currentTenant?.ConnectionString;
        }

        public Tenant GetTenant()
        {
            return _currentTenant;
        }
    }
}