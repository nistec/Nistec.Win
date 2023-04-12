using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Nistec.Printing.Data
{
    public interface IAdoMap
    {

        bool CanLink{get;}
        string Description{get;}
        bool IsLinked{get;}
        object Value{get;}
        string Name{ get;}
        AdoMap Parent{ get;}
        AdoProperties Properties{ get;}
        string ErrorDescription{ get;}
      }
    //public interface IAdoOutput//:IAdoMap
    //{
    //    //AdoOutput Output{ get;}
    //    string Description { get;}
    //    object Value { get;}
    //    string Name { get;}
    //}
    public interface IAdoOutput //: IAdoMap
    {
        //AdoInput Input { get;}
        string Description { get;}
        object Value { get;}
        string Name { get;}
        bool HasErrors { get;}
        string ErrorDescription { get;}

    }


    public interface IAdoReadProperties 
    {
        string Query { get;set;}
        QueryType QueryType { get;set;}
        //string ConnectionString { get;set;}
    }

    public interface IAdoReader
    {
        void ExecuteBegin(uint totalObjects);
        uint ExecuteCommit();
        bool ExecuteBatch(uint batchSize);
        bool Execute();
        void CancelExecute(string message);

        AdoProperties Properties { get;set;}
        //AdoOutput Output { get;}
        AdoOutput Output { get; }

        //bool HasErrors { get;}
        //bool ExecutionStoped { get;}
        //string ErrorDescription { get;}
        //string ResultMessage { get;}

    }

    public interface IAdoWriter
    {
        void ExecuteBegin(uint totalObjects);
        uint ExecuteCommit();
        bool ExecuteBatch(uint batchSize);
        bool Execute();
        void CancelExecute(string message);

        AdoProperties Properties { get;set;}
        //AdoInput Input { get;}
        AdoOutput Output { get; }

        //bool HasErrors { get;}
        //bool ExecutionStoped { get;}
        //string ErrorDescription { get;}
        //string ResultMessage { get;}

    }

}
