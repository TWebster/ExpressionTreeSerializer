using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace ExpressionSerialization
{
	//GenerateXmlFromExpressionCore
	public partial class ExpressionSerializer
    {
        
		public XElement GenerateXmlFromExpressionCore(Expression e)
        {
            if (e == null)
                return null;
            XElement replace = ApplyCustomConverters(e);
            if (replace != null)
                return replace;

			//string name = GetNameOfExpression(e);
			string name = e.NodeType.ToString();
			object[] XElementValues = null;
			dynamic value;
						
			if (e is System.Linq.Expressions.BinaryExpression )
			{			
				XElementValues = new object[9];
				value = ((System.Linq.Expressions.BinaryExpression)e).CanReduce;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
				value = ((System.Linq.Expressions.BinaryExpression)e).Right;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Right", value ?? string.Empty);
				value = ((System.Linq.Expressions.BinaryExpression)e).Left;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Left", value ?? string.Empty);
				value = ((System.Linq.Expressions.BinaryExpression)e).Method;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Reflection.MethodInfo), 
					"Method", value ?? string.Empty);
				value = ((System.Linq.Expressions.BinaryExpression)e).Conversion;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.LambdaExpression), 
					"Conversion", value ?? string.Empty);
				value = ((System.Linq.Expressions.BinaryExpression)e).IsLifted;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"IsLifted", value ?? string.Empty);
				value = ((System.Linq.Expressions.BinaryExpression)e).IsLiftedToNull;
				XElementValues[6] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"IsLiftedToNull", value ?? string.Empty);
				value = ((System.Linq.Expressions.BinaryExpression)e).NodeType;
				XElementValues[7] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.BinaryExpression)e).Type;
				XElementValues[8] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.BlockExpression )
			{			
				XElementValues = new object[6];
				value = ((System.Linq.Expressions.BlockExpression)e).Expressions;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>), 
					"Expressions", value ?? string.Empty);
				value = ((System.Linq.Expressions.BlockExpression)e).Variables;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression>), 
					"Variables", value ?? string.Empty);
				value = ((System.Linq.Expressions.BlockExpression)e).Result;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Result", value ?? string.Empty);
				value = ((System.Linq.Expressions.BlockExpression)e).NodeType;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.BlockExpression)e).Type;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.BlockExpression)e).CanReduce;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.ConditionalExpression )
			{			
				XElementValues = new object[6];
				value = ((System.Linq.Expressions.ConditionalExpression)e).NodeType;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.ConditionalExpression)e).Type;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.ConditionalExpression)e).Test;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Test", value ?? string.Empty);
				value = ((System.Linq.Expressions.ConditionalExpression)e).IfTrue;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"IfTrue", value ?? string.Empty);
				value = ((System.Linq.Expressions.ConditionalExpression)e).IfFalse;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"IfFalse", value ?? string.Empty);
				value = ((System.Linq.Expressions.ConditionalExpression)e).CanReduce;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.ConstantExpression )
			{			
				XElementValues = new object[4];
				value = ((System.Linq.Expressions.ConstantExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.ConstantExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.ConstantExpression)e).Value;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Object), 
					"Value", value ?? string.Empty);
				value = ((System.Linq.Expressions.ConstantExpression)e).CanReduce;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.DebugInfoExpression )
			{			
				XElementValues = new object[9];
				value = ((System.Linq.Expressions.DebugInfoExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.DebugInfoExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.DebugInfoExpression)e).StartLine;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Int32), 
					"StartLine", value ?? string.Empty);
				value = ((System.Linq.Expressions.DebugInfoExpression)e).StartColumn;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Int32), 
					"StartColumn", value ?? string.Empty);
				value = ((System.Linq.Expressions.DebugInfoExpression)e).EndLine;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Int32), 
					"EndLine", value ?? string.Empty);
				value = ((System.Linq.Expressions.DebugInfoExpression)e).EndColumn;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Int32), 
					"EndColumn", value ?? string.Empty);
				value = ((System.Linq.Expressions.DebugInfoExpression)e).Document;
				XElementValues[6] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.SymbolDocumentInfo), 
					"Document", value ?? string.Empty);
				value = ((System.Linq.Expressions.DebugInfoExpression)e).IsClear;
				XElementValues[7] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"IsClear", value ?? string.Empty);
				value = ((System.Linq.Expressions.DebugInfoExpression)e).CanReduce;
				XElementValues[8] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.DefaultExpression )
			{			
				XElementValues = new object[3];
				value = ((System.Linq.Expressions.DefaultExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.DefaultExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.DefaultExpression)e).CanReduce;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.DynamicExpression )
			{			
				XElementValues = new object[6];
				value = ((System.Linq.Expressions.DynamicExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.DynamicExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.DynamicExpression)e).Binder;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Runtime.CompilerServices.CallSiteBinder), 
					"Binder", value ?? string.Empty);
				value = ((System.Linq.Expressions.DynamicExpression)e).DelegateType;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Type), 
					"DelegateType", value ?? string.Empty);
				value = ((System.Linq.Expressions.DynamicExpression)e).Arguments;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>), 
					"Arguments", value ?? string.Empty);
				value = ((System.Linq.Expressions.DynamicExpression)e).CanReduce;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.GotoExpression )
			{			
				XElementValues = new object[6];
				value = ((System.Linq.Expressions.GotoExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.GotoExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.GotoExpression)e).Value;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Value", value ?? string.Empty);
				value = ((System.Linq.Expressions.GotoExpression)e).Target;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.LabelTarget), 
					"Target", value ?? string.Empty);
				value = ((System.Linq.Expressions.GotoExpression)e).Kind;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.GotoExpressionKind), 
					"Kind", value ?? string.Empty);
				value = ((System.Linq.Expressions.GotoExpression)e).CanReduce;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.IndexExpression )
			{			
				XElementValues = new object[6];
				value = ((System.Linq.Expressions.IndexExpression)e).NodeType;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.IndexExpression)e).Type;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.IndexExpression)e).Object;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Object", value ?? string.Empty);
				value = ((System.Linq.Expressions.IndexExpression)e).Indexer;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Reflection.PropertyInfo), 
					"Indexer", value ?? string.Empty);
				value = ((System.Linq.Expressions.IndexExpression)e).Arguments;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>), 
					"Arguments", value ?? string.Empty);
				value = ((System.Linq.Expressions.IndexExpression)e).CanReduce;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.InvocationExpression )
			{			
				XElementValues = new object[5];
				value = ((System.Linq.Expressions.InvocationExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.InvocationExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.InvocationExpression)e).Expression;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Expression", value ?? string.Empty);
				value = ((System.Linq.Expressions.InvocationExpression)e).Arguments;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>), 
					"Arguments", value ?? string.Empty);
				value = ((System.Linq.Expressions.InvocationExpression)e).CanReduce;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.LabelExpression )
			{			
				XElementValues = new object[5];
				value = ((System.Linq.Expressions.LabelExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.LabelExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.LabelExpression)e).Target;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.LabelTarget), 
					"Target", value ?? string.Empty);
				value = ((System.Linq.Expressions.LabelExpression)e).DefaultValue;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"DefaultValue", value ?? string.Empty);
				value = ((System.Linq.Expressions.LabelExpression)e).CanReduce;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.LambdaExpression )
			{			
				XElementValues = new object[8];
				value = ((System.Linq.Expressions.LambdaExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.LambdaExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.LambdaExpression)e).Parameters;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression>), 
					"Parameters", value ?? string.Empty);
				value = ((System.Linq.Expressions.LambdaExpression)e).Name;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.String), 
					"Name", value ?? string.Empty);
				value = ((System.Linq.Expressions.LambdaExpression)e).Body;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Body", value ?? string.Empty);
				value = ((System.Linq.Expressions.LambdaExpression)e).ReturnType;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Type), 
					"ReturnType", value ?? string.Empty);
				value = ((System.Linq.Expressions.LambdaExpression)e).TailCall;
				XElementValues[6] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"TailCall", value ?? string.Empty);
				value = ((System.Linq.Expressions.LambdaExpression)e).CanReduce;
				XElementValues[7] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.ListInitExpression )
			{			
				XElementValues = new object[5];
				value = ((System.Linq.Expressions.ListInitExpression)e).NodeType;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.ListInitExpression)e).Type;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.ListInitExpression)e).CanReduce;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
				value = ((System.Linq.Expressions.ListInitExpression)e).NewExpression;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.NewExpression), 
					"NewExpression", value ?? string.Empty);
				value = ((System.Linq.Expressions.ListInitExpression)e).Initializers;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ElementInit>), 
					"Initializers", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.LoopExpression )
			{			
				XElementValues = new object[6];
				value = ((System.Linq.Expressions.LoopExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.LoopExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.LoopExpression)e).Body;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Body", value ?? string.Empty);
				value = ((System.Linq.Expressions.LoopExpression)e).BreakLabel;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.LabelTarget), 
					"BreakLabel", value ?? string.Empty);
				value = ((System.Linq.Expressions.LoopExpression)e).ContinueLabel;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.LabelTarget), 
					"ContinueLabel", value ?? string.Empty);
				value = ((System.Linq.Expressions.LoopExpression)e).CanReduce;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.MemberExpression )
			{			
				XElementValues = new object[5];
				value = ((System.Linq.Expressions.MemberExpression)e).Member;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Reflection.MemberInfo), 
					"Member", value ?? string.Empty);
				value = ((System.Linq.Expressions.MemberExpression)e).Expression;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Expression", value ?? string.Empty);
				value = ((System.Linq.Expressions.MemberExpression)e).NodeType;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.MemberExpression)e).Type;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.MemberExpression)e).CanReduce;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.MemberInitExpression )
			{			
				XElementValues = new object[5];
				value = ((System.Linq.Expressions.MemberInitExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.MemberInitExpression)e).CanReduce;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
				value = ((System.Linq.Expressions.MemberInitExpression)e).NodeType;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.MemberInitExpression)e).NewExpression;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.NewExpression), 
					"NewExpression", value ?? string.Empty);
				value = ((System.Linq.Expressions.MemberInitExpression)e).Bindings;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.MemberBinding>), 
					"Bindings", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.MethodCallExpression )
			{			
				XElementValues = new object[6];
				value = ((System.Linq.Expressions.MethodCallExpression)e).NodeType;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.MethodCallExpression)e).Type;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.MethodCallExpression)e).Method;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Reflection.MethodInfo), 
					"Method", value ?? string.Empty);
				value = ((System.Linq.Expressions.MethodCallExpression)e).Object;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Object", value ?? string.Empty);
				value = ((System.Linq.Expressions.MethodCallExpression)e).Arguments;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>), 
					"Arguments", value ?? string.Empty);
				value = ((System.Linq.Expressions.MethodCallExpression)e).CanReduce;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.NewArrayExpression )
			{			
				XElementValues = new object[4];
				value = ((System.Linq.Expressions.NewArrayExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.NewArrayExpression)e).Expressions;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>), 
					"Expressions", value ?? string.Empty);
				value = ((System.Linq.Expressions.NewArrayExpression)e).NodeType;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.NewArrayExpression)e).CanReduce;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.NewExpression )
			{			
				XElementValues = new object[6];
				value = ((System.Linq.Expressions.NewExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.NewExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.NewExpression)e).Constructor;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Reflection.ConstructorInfo), 
					"Constructor", value ?? string.Empty);
				value = ((System.Linq.Expressions.NewExpression)e).Arguments;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression>), 
					"Arguments", value ?? string.Empty);
				value = ((System.Linq.Expressions.NewExpression)e).Members;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Reflection.MemberInfo>), 
					"Members", value ?? string.Empty);
				value = ((System.Linq.Expressions.NewExpression)e).CanReduce;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.ParameterExpression )
			{			
				XElementValues = new object[5];
				value = ((System.Linq.Expressions.ParameterExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.ParameterExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.ParameterExpression)e).Name;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.String), 
					"Name", value ?? string.Empty);
				value = ((System.Linq.Expressions.ParameterExpression)e).IsByRef;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"IsByRef", value ?? string.Empty);
				value = ((System.Linq.Expressions.ParameterExpression)e).CanReduce;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.RuntimeVariablesExpression )
			{			
				XElementValues = new object[4];
				value = ((System.Linq.Expressions.RuntimeVariablesExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.RuntimeVariablesExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.RuntimeVariablesExpression)e).Variables;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression>), 
					"Variables", value ?? string.Empty);
				value = ((System.Linq.Expressions.RuntimeVariablesExpression)e).CanReduce;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.SwitchExpression )
			{			
				XElementValues = new object[7];
				value = ((System.Linq.Expressions.SwitchExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.SwitchExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.SwitchExpression)e).SwitchValue;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"SwitchValue", value ?? string.Empty);
				value = ((System.Linq.Expressions.SwitchExpression)e).Cases;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.SwitchCase>), 
					"Cases", value ?? string.Empty);
				value = ((System.Linq.Expressions.SwitchExpression)e).DefaultBody;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"DefaultBody", value ?? string.Empty);
				value = ((System.Linq.Expressions.SwitchExpression)e).Comparison;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Reflection.MethodInfo), 
					"Comparison", value ?? string.Empty);
				value = ((System.Linq.Expressions.SwitchExpression)e).CanReduce;
				XElementValues[6] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.TryExpression )
			{			
				XElementValues = new object[7];
				value = ((System.Linq.Expressions.TryExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.TryExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.TryExpression)e).Body;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Body", value ?? string.Empty);
				value = ((System.Linq.Expressions.TryExpression)e).Handlers;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.CatchBlock>), 
					"Handlers", value ?? string.Empty);
				value = ((System.Linq.Expressions.TryExpression)e).Finally;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Finally", value ?? string.Empty);
				value = ((System.Linq.Expressions.TryExpression)e).Fault;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Fault", value ?? string.Empty);
				value = ((System.Linq.Expressions.TryExpression)e).CanReduce;
				XElementValues[6] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.TypeBinaryExpression )
			{			
				XElementValues = new object[5];
				value = ((System.Linq.Expressions.TypeBinaryExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.TypeBinaryExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.TypeBinaryExpression)e).Expression;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Expression", value ?? string.Empty);
				value = ((System.Linq.Expressions.TypeBinaryExpression)e).TypeOperand;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Type), 
					"TypeOperand", value ?? string.Empty);
				value = ((System.Linq.Expressions.TypeBinaryExpression)e).CanReduce;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			
			if (e is System.Linq.Expressions.UnaryExpression )
			{			
				XElementValues = new object[7];
				value = ((System.Linq.Expressions.UnaryExpression)e).Type;
				XElementValues[0] = GenerateXmlFromProperty(typeof(System.Type), 
					"Type", value ?? string.Empty);
				value = ((System.Linq.Expressions.UnaryExpression)e).NodeType;
				XElementValues[1] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.ExpressionType), 
					"NodeType", value ?? string.Empty);
				value = ((System.Linq.Expressions.UnaryExpression)e).Operand;
				XElementValues[2] = GenerateXmlFromProperty(typeof(System.Linq.Expressions.Expression), 
					"Operand", value ?? string.Empty);
				value = ((System.Linq.Expressions.UnaryExpression)e).Method;
				XElementValues[3] = GenerateXmlFromProperty(typeof(System.Reflection.MethodInfo), 
					"Method", value ?? string.Empty);
				value = ((System.Linq.Expressions.UnaryExpression)e).IsLifted;
				XElementValues[4] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"IsLifted", value ?? string.Empty);
				value = ((System.Linq.Expressions.UnaryExpression)e).IsLiftedToNull;
				XElementValues[5] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"IsLiftedToNull", value ?? string.Empty);
				value = ((System.Linq.Expressions.UnaryExpression)e).CanReduce;
				XElementValues[6] = GenerateXmlFromProperty(typeof(System.Boolean), 
					"CanReduce", value ?? string.Empty);
			}

			return new XElement(name, XElementValues);
			//return new XElement(
			//            GetNameOfExpression(e),
			//            from prop in e.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
			//            select GenerateXmlFromProperty(prop.PropertyType, prop.Name, prop.GetValue(e, null)));
        }


	}//end ExpressionSerializer class
}
