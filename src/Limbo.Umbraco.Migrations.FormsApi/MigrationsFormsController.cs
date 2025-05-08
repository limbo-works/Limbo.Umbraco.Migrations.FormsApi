using System.Collections.Generic;
using System;
using System.Linq;
using System.Web.Http;
using Limbo.Umbraco.MigrationsApi.Controllers;
using Newtonsoft.Json.Linq;
using Umbraco.Forms.Core.Data.Storage;
using Umbraco.Forms.Core.Models;
using Umbraco.Forms.Core.Persistence.Dtos;
using Newtonsoft.Json;
using Umbraco.Forms.Core.Interfaces;
using Newtonsoft.Json.Serialization;
using System.Web.Configuration;
using Skybrud.Essentials.Strings.Extensions;

#pragma warning disable CS0618 // Type or member is obsolete

namespace Limbo.Umbraco.Migrations.FormsApi {

    public class MigrationsFormsController : MigrationsControllerBase {

        private readonly IFormStorage _formStorage;
        private readonly IWorkflowStorage _workflowStorage;
        private readonly IRecordStorage _recordStorage;
        private readonly JsonSerializer _jsonSerializer;

        public MigrationsFormsController(IFormStorage formStorage, IWorkflowStorage workflowStorage, IRecordStorage recordStorage) {
            _formStorage = formStorage;
            _workflowStorage = workflowStorage;
            _recordStorage = recordStorage;
            _jsonSerializer = new JsonSerializer {
                ContractResolver = new DefaultContractResolver {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            };
        }

        [HttpGet]
        [Route("api/limbo/migrations/forms")]
        public object Index() {
            if (!HasAccess(out string reason)) return Unauthorized(reason);
            JArray array = new JArray();
            int total = 0;
            foreach (Form form in _formStorage.GetAllForms()) {
                array.Add(Serialize(form));
                total++;
            }
            return new JObject {
                {"total", total},
                {"items", array}
            };
        }

        [HttpGet]
        [Route("api/limbo/migrations/forms/{key}")]
        public object GetForm(Guid key) {
            if (!HasAccess(out string reason)) return Unauthorized(reason);
            Form form = _formStorage.GetForm(key);
            return Serialize(form);
        }

        [HttpGet]
        [Route("api/limbo/migrations/forms/{key}/records")]
        public object GetFormRecords(Guid key) {
            if (!HasAccess(out string reason)) return Unauthorized(reason);
            Form form = _formStorage.GetForm(key);
            if (form == null) return NotFound();
            List<Record> records = _recordStorage.GetAllRecords(form);
            return new JObject {
                {"total", records.Count},
                {"items", new JArray(records.Select(Serialize))}
            };
        }

        private JObject Serialize(Form form) {
            JObject json = JObject.FromObject(form, _jsonSerializer);
            JArray workflows = new JArray();
            foreach (Guid workflowKey in form.WorkflowIds) {
                IWorkflow workflow = _workflowStorage.GetWorkflow(workflowKey);
                if (workflow is null) { continue; }
                workflows.Add(JObject.FromObject(workflow));
            }
            json["workflows"] = workflows;
            return json;
        }

        private JObject Serialize(IRecord record) {
            return JObject.FromObject(record, _jsonSerializer);
        }

        protected override bool HasAccess(out string rejectionReason) {

            if (!base.HasAccess(out rejectionReason)) return false;

            bool enabled = WebConfigurationManager.AppSettings["LimboMigrationsApiFormsEnabled"].ToBoolean();
            if (enabled) return true;

            rejectionReason = "Forms controller has not been enabled.";
            return false;

        }

    }

}