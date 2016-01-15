# Ninject.Web.Mvc6
Unofficial Mvc6 integration for Ninject with Lazy/Func support.

Ninject integration that used to be shipped with ASP.NET 5 didn't properly support Ninject Factory features: scope paramaters were not propogated correctly. This is a fixed clone that allows to use Ninject with ASP.NET 5 RC1 and Func<>/Lazy<> imports.