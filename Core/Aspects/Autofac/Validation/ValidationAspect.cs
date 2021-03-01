﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir Validator Değildir.");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }


        //protected virtual void OnBefore(IInvocation invocation) { }
        //protected virtual void OnAfter(IInvocation invocation) { }
        //protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        //protected virtual void OnSuccess(IInvocation invocation) { }
        //public override void Intercept(IInvocation invocation)
        //{
        //    var isSuccess = true;
        //    OnBefore(invocation);
        //    try
        //    {
        //        invocation.Proceed();
        //    }
        //    catch (Exception e)
        //    {
        //        isSuccess = false;
        //        OnException(invocation, e);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (isSuccess)
        //        {
        //            OnSuccess(invocation);
        //        }
        //    }
        //    OnAfter(invocation);
        //}


    }
}