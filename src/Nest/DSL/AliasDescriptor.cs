﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesUpdateAliases")]
	public partial class AliasDescriptor : 
		 IPathInfo<AliasRequestParameters>
	{
		public AliasDescriptor()
		{
			this._Actions = new List<IAliasAction>();
		}

		[JsonProperty("actions")]
		internal IList<IAliasAction> _Actions { get; set; }

		public AliasDescriptor Add(Func<AliasAddDescriptor, AliasAddDescriptor> addSelector)
		{
			addSelector.ThrowIfNull("addSelector");
			var descriptor = addSelector(new AliasAddDescriptor());
			descriptor.ThrowIfNull("addAliasDescriptor");
			this._Actions.Add(descriptor);
			return this;
		}
		public AliasDescriptor Remove(Func<AliasRemoveDescriptor, AliasRemoveDescriptor> removeSelector)
		{
			removeSelector.ThrowIfNull("removeSelector");
			var descriptor = removeSelector(new AliasRemoveDescriptor());
			descriptor.ThrowIfNull("removeAliasDescriptor");
			this._Actions.Add(descriptor);
			return this;
		}

		ElasticsearchPathInfo<AliasRequestParameters> IPathInfo<AliasRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = new ElasticsearchPathInfo<AliasRequestParameters>();
			pathInfo.RequestParameters = this._QueryString;
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}
