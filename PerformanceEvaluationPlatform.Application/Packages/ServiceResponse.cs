using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PerformanceEvaluationPlatform.Application.Helpers;

namespace PerformanceEvaluationPlatform.Application.Packages
{
	public class ServiceResponse {

		public const string GeneralErrorProperty = "_";

		protected ServiceResponse(){}

		protected Dictionary<string, ICollection<string>> _errors;
		protected int _statusCode;

		public bool IsValid => _errors == null || !_errors.Any();
		public Dictionary<string, ICollection<string>> Errors => _errors;

		public int StatusCode => _statusCode;

		public static ServiceResponse Success(int statusCode = 200) =>
			new ServiceResponse
			{
				_statusCode = statusCode,
				_errors = null
			};

		public static ServiceResponse Failure(Dictionary<string, ICollection<string>> errors, int statusCode = 400) =>
			new ServiceResponse
			{
				_statusCode = statusCode,
				_errors = errors
			};

		public static ServiceResponse Failure<T>(Expression<Func<T, object>> property, string message, int statusCode = 400)
		{
			return Failure(ExpressionHelper.GetPropertyName(property), message);
		}

		public static ServiceResponse Failure(string propertyName, string message, int statusCode = 400) =>
			Failure(new Dictionary<string, ICollection<string>> {
				{propertyName, new List<string>{message}}
			}, statusCode);

		public static ServiceResponse Failure(string message, int statusCode = 400) =>
			Failure(GeneralErrorProperty, message, statusCode);

		public static ServiceResponse BadRequest(Dictionary<string, ICollection<string>> errors) =>
			new ServiceResponse
			{
				_statusCode = 400,
				_errors = errors
			};

		public static ServiceResponse BadRequest(string message = ServiceResponse.GeneralErrorProperty) =>
			Failure(message, 400);

		public static ServiceResponse Forbidden(string message = ServiceResponse.GeneralErrorProperty) =>
			Failure(message, 403);

		public static ServiceResponse NotFound(string message = ServiceResponse.GeneralErrorProperty) =>
			Failure(message, 404);

		public static ServiceResponse Conflict(string message = ServiceResponse.GeneralErrorProperty) =>
			Failure(message, 409);

		public static ServiceResponse UnprocessableEntity(string message = ServiceResponse.GeneralErrorProperty) =>
			Failure(message, 422);

        public static ServiceResponse NoContent() => 
			Success(204);

        public bool IsBadRequest => StatusCode == 400;
		public bool IsForbidden => StatusCode == 403;
		public bool IsNotFound => StatusCode == 404;
		public bool IsConflict => StatusCode == 409;
		public bool IsUnprocessableEntity => StatusCode == 422;
	}

	public class ServiceResponse<TResponse> : ServiceResponse
	{
		private TResponse _payload;

		public TResponse Payload {
			get {
				if (!IsValid) throw new ServiceResponseInvalidException();
				return _payload;
			}
		}

		protected ServiceResponse(){}

		public static ServiceResponse<TResponse> Success(TResponse payload, int statusCode = 200) => 
			new ServiceResponse<TResponse> {
				_statusCode = statusCode,
				_errors = null, 
				_payload = payload
			};

		public new static ServiceResponse<TResponse> Failure(Dictionary<string, ICollection<string>> errors, int statusCode = 400) =>
			new ServiceResponse<TResponse>
			{
				_statusCode = statusCode,
				_errors = errors,
				_payload = default
			};

		public new static ServiceResponse<TResponse> BadRequest(Dictionary<string, ICollection<string>> errors) {
			return new ServiceResponse<TResponse> {
				_statusCode = 400,
				_errors = errors,
				_payload = default
			};
		}

		public new static ServiceResponse<TResponse> Failure(string message, int statusCode = 400) =>
			Failure(new Dictionary<string, ICollection<string>> {
				{GeneralErrorProperty, new List<string>{message}}
			}, statusCode);

		public static ServiceResponse<TResponse> Conflict<T>(Expression<Func<T, object>> property, string message, int statusCode = 400)
		{
			return Conflict(ExpressionHelper.GetPropertyName(property), message);
		}

		public static ServiceResponse<TResponse> Conflict(string propertyName, string message, int statusCode = 409) =>
			Failure(new Dictionary<string, ICollection<string>> {
				{propertyName, new List<string>{message}}
			}, statusCode);

		public new static ServiceResponse<TResponse> BadRequest(string message = ServiceResponse.GeneralErrorProperty) =>
			Failure(message, 400);

		public new static ServiceResponse<TResponse> Forbidden(string message = ServiceResponse.GeneralErrorProperty) =>
			Failure(message, 403);

		public new static ServiceResponse<TResponse> NotFound(string message = ServiceResponse.GeneralErrorProperty) =>
			Failure(message, 404);

		public new static ServiceResponse<TResponse> Conflict(string message = ServiceResponse.GeneralErrorProperty) =>
			Failure(message, 409);
	}
}
