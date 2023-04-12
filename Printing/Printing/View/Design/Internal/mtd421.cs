namespace MControl.Printing.View.Design
{
    using EnvDTE;
    using System;
    using System.CodeDom;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.IO;

    //mtd421
    internal class UICodeDomSerializer : CodeDomSerializer
    {


        public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
        {
            // This is how we associate the component with the serializer.
            CodeDomSerializer serializer = (CodeDomSerializer)manager.
            GetSerializer(typeof(Report).BaseType, typeof(CodeDomSerializer));

            return serializer.Deserialize(manager, codeObject);
        }

        //public override object Deserialize(IDesignerSerializationManager var0, object var1)
        //{

        //    TypeCodeDomSerializer serializer = this.var2(var0);
        //    if (serializer == null)
        //    {
        //        return null;
        //    }
        //    CodeTypeDeclaration declaration = (CodeTypeDeclaration) var1;
        //    if (!var3(var0))
        //    {
        //        return serializer.Deserialize(var0, declaration);
        //    }
        //    ProjectItem item = (ProjectItem) var0.GetService(typeof(ProjectItem));
        //    string str = this.var4(declaration);
        //    if ((str == null) || (str.Length == 0))
        //    {
        //        str = var5(item.Properties, "FileName");
        //    }
        //    str = Path.Combine("App_GlobalResources", str);
        //    str = Path.Combine(var6(item.ContainingProject), str);
        //    if (!File.Exists(str))
        //    {
        //        return serializer.Deserialize(var0, declaration);
        //    }
        //    IServiceContainer container = (IServiceContainer) var0.GetService(typeof(IServiceContainer));
        //    IResourceService serviceInstance = (IResourceService) var0.GetService(typeof(IResourceService));
        //    container.RemoveService(typeof(IResourceService));
        //    mtd420 mtd = new mtd420(str);
        //    container.AddService(typeof(IResourceService), mtd);
        //    object obj2 = null;
        //    try
        //    {
        //        obj2 = serializer.Deserialize(var0, declaration);
        //    }
        //    catch
        //    {
        //        obj2 = null;
        //    }
        //    finally
        //    {
        //        container.RemoveService(typeof(IResourceService));
        //        mtd.Dispose();
        //        container.AddService(typeof(IResourceService), serviceInstance);
        //    }
        //    return obj2;
        //}

        public override object Serialize(IDesignerSerializationManager var0, object var7)
        {
            TypeCodeDomSerializer serializer = this.var2(var0);
            if (serializer == null)
            {
                return null;
            }
            IDesignerHost host = (IDesignerHost) var0.GetService(typeof(IDesignerHost));
            if (!var3(var0))
            {
                return serializer.Serialize(var0, var7, host.Container.Components);
            }
            object obj2 = null;
            IServiceContainer container = (IServiceContainer) var0.GetService(typeof(IServiceContainer));
            IResourceService serviceInstance = (IResourceService) var0.GetService(typeof(IResourceService));
            container.RemoveService(typeof(IResourceService));
            mtd420 mtd = new mtd420(var6(this.var8(var0)));
            container.AddService(typeof(IResourceService), mtd);
            try
            {
                obj2 = serializer.Serialize(var0, var7, host.Container.Components);
                if (var3(var0))
                {
                    this.var9(var0, obj2);
                }
            }
            catch
            {
                obj2 = null;
            }
            finally
            {
                container.RemoveService(typeof(IResourceService));
                mtd.Dispose();
                container.AddService(typeof(IResourceService), serviceInstance);
            }
            return obj2;
        }

        private CodeMemberMethod var10(CodeTypeDeclaration var11, string var12)
        {
            foreach (CodeTypeMember member in var11.Members)
            {
                if ((member is CodeMemberMethod) && (member.Name == var12))
                {
                    return (member as CodeMemberMethod);
                }
            }
            return null;
        }

        private TypeCodeDomSerializer var2(IDesignerSerializationManager manager)
        {
            return (manager.GetSerializer(typeof(Report), typeof(TypeCodeDomSerializer)) as TypeCodeDomSerializer);
        }

        private CodeStatement var20(CodeStatementCollection var22, string var23)
        {
            for (int i = 0; i < var22.Count; i++)
            {
                CodeStatement statement = var22[i];
                if ((statement is CodeVariableDeclarationStatement) && (((CodeVariableDeclarationStatement) statement).Name == var23))
                {
                    return statement;
                }
            }
            return null;
        }

        private static bool var21(IServiceProvider var18)
        {
            ProjectItem service = var18.GetService(typeof(ProjectItem)) as ProjectItem;
            return ((service != null) && (Path.GetExtension(service.Name) == ".cs"));
        }

        private static bool var3(IServiceProvider var13)
        {
            if (var13 != null)
            {
                ProjectItem service = (ProjectItem) var13.GetService(typeof(ProjectItem));
                if ((service != null) && (service.ContainingProject.Kind.ToUpper() == "{E24C65DC-7377-472B-9ABA-BC803B73C61A}"))
                {
                    return true;
                }
            }
            return false;
        }

        private string var4(CodeTypeDeclaration var24)
        {
            CodeMemberMethod method = this.var10(var24, "InitializeComponent");
            CodeVariableDeclarationStatement statement = (method != null) ? (this.var20(method.Statements, "resourceFileName") as CodeVariableDeclarationStatement) : null;
            if (statement != null)
            {
                CodePrimitiveExpression initExpression = statement.InitExpression as CodePrimitiveExpression;
                if ((initExpression != null) && (initExpression.Value is string))
                {
                    return (string)initExpression.Value;
                }
            }
            return string.Empty;
        }

        private static string var5(Properties var16, string var17)
        {
            try
            {
                return (string) var16.Item(var17).Value;
            }
            catch
            {
            }
            return string.Empty;
        }

        private static string var6(Project var14)
        {
            if (var14 == null)
            {
                return string.Empty;
            }
            return var5(var14.Properties, "FullPath");
        }

        private static string var6(ProjectItem var15)
        {
            if (var15 == null)
            {
                return string.Empty;
            }
            return var5(var15.Properties, "FullPath");
        }

        private ProjectItem var8(IServiceProvider serviceProvider)
        {
            ProjectItem service = (ProjectItem) serviceProvider.GetService(typeof(ProjectItem));
            string index = Path.ChangeExtension(var5(service.Properties, "FileName"), "resx");
            Project containingProject = service.ContainingProject;
            ProjectItem item2 = null;
            try
            {
                item2 = containingProject.ProjectItems.Item("App_GlobalResources");
            }
            catch
            {
            }
            if (item2 == null)
            {
                item2 = containingProject.ProjectItems.AddFolder("App_GlobalResources", "");
            }
            ProjectItem item3 = null;
            try
            {
                item3 = item2.ProjectItems.Item(index);
            }
            catch
            {
            }
            if (item3 == null)
            {
                index = Path.Combine(var6(item2), index);
                File.Create(index).Close();
                item3 = item2.ProjectItems.AddFromFile(index);
            }
            if ((item3 != null) && (item3.Document != null))
            {
                item3.Document.Close(vsSaveChanges.vsSaveChangesNo);
            }
            return item3;
        }

        private void var9(IServiceProvider var18, object var19)
        {
            ProjectItem service = (ProjectItem) var18.GetService(typeof(ProjectItem));
            string path = var5(service.Properties, "FileName");
            CodeTypeDeclaration declaration = var19 as CodeTypeDeclaration;
            string type = "Resources." + Path.GetFileNameWithoutExtension(path);
            path = Path.ChangeExtension(path, ".resx");
            if (declaration != null)
            {
                CodeMemberMethod method = this.var10(declaration, "InitializeComponent");
                CodeStatement statement = this.var20(method.Statements, "resources");
                if (statement != null)
                {
                    method.Statements.Remove(statement);
                    if (var21(var18))
                    {
                        CodeVariableDeclarationStatement statement2 = new CodeVariableDeclarationStatement("System.Resources.ResourceManager", "resources");
                        CodePropertyReferenceExpression expression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(type), "ResourceManager");
                        statement2.InitExpression = expression;
                        method.Statements.Insert(0, statement2);
                    }
                    else
                    {
                        CodeMemberMethod method2 = new CodeMemberMethod();
                        method2.Name = "GetResourceManager";
                        method2.ReturnType = new CodeTypeReference("System.Resources.ResourceManager");
                        CodePropertyReferenceExpression expression2 = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(type), "ResourceManager");
                        method2.Statements.Add(new CodeMethodReturnStatement(expression2));
                        declaration.Members.Add(method2);
                        CodeVariableDeclarationStatement statement3 = new CodeVariableDeclarationStatement("System.Resources.ResourceManager", "resources");
                        CodeMethodInvokeExpression expression3 = new CodeMethodInvokeExpression();
                        expression3.Method.MethodName = "GetResourceManager";
                        statement3.InitExpression = expression3;
                        method.Statements.Insert(0, statement3);
                    }
                }
                if (method != null)
                {
                    CodeVariableDeclarationStatement statement4 = new CodeVariableDeclarationStatement(typeof(string), "resourceFileName");
                    statement4.InitExpression = new CodePrimitiveExpression(path);
                    method.Statements.Insert(0, statement4);
                }
            }
        }
    }
}

