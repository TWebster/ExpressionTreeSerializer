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
    public class ExpressionSerializer
    {
        private static readonly Type[] attributeTypes = new[] { typeof(string), typeof(int), typeof(bool), typeof(ExpressionType) };
        private Dictionary<string, ParameterExpression> parameters = new Dictionary<string, ParameterExpression>();
        private ExpressionSerializationTypeResolver resolver;
        public List<CustomExpressionXmlConverter> Converters { get; private set; }

        public ExpressionSerializer(ExpressionSerializationTypeResolver resolver)
        {
            this.resolver = resolver;
            Converters = new List<CustomExpressionXmlConverter>();
        }

        public ExpressionSerializer()
        {
            this.resolver = new ExpressionSerializationTypeResolver();
            Converters = new List<CustomExpressionXmlConverter>();
        }

        
        
        /*
         * SERIALIZATION 
         */

        public XElement Serialize(Expression e)
        {
            return GenerateXmlFromExpressionCore(e);
        }

#if SILVERLIGHT
		public XElement GenerateXmlFromExpressionCore(Expression e)
        {
            if (e == null)
                return null;
            XElement replace = ApplyCustomConverters(e);
            if (replace != null)
                return replace;

			string name = GetNameOfExpression(e);
			object[] XElementValues = null;
			object value;
						
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
#else
		private XElement GenerateXmlFromExpressionCore(Expression e)
		{
			if (e == null)
				return null;
			XElement replace = ApplyCustomConverters(e);
			if (replace != null)
				return replace;
			string expressionName = GetNameOfExpression(e);

			PropertyInfo[] propertyInfos = e.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			object[] XELementValues = new object[propertyInfos.Length];
			for (int i = 0; i < propertyInfos.Length; i++)
			{
				PropertyInfo p = propertyInfos[i];
				object value = GenerateXmlFromProperty(p.PropertyType, p.Name, p.GetValue(e, null));
				XELementValues[i] = value;
			}
			return new XElement(expressionName, XELementValues);
			//return new XElement(
			//            expressionName,
			//            from prop in e.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
			//            select GenerateXmlFromProperty(prop.PropertyType, prop.Name, prop.GetValue(e, null)));
		}
#endif
       

        private XElement ApplyCustomConverters(Expression e)
        {
            foreach (var converter in Converters)
            {
                XElement result = converter.Serialize(e);
                if (result != null)
                    return result;
            }
            return null;
        }

        private string GetNameOfExpression(Expression e)
        {
			string name;
            if (e is LambdaExpression)
                name = "LambdaExpression";
            else
				name =  ToGenericTypeNameString(e.GetType());
			return name;
        }

        private object GenerateXmlFromProperty(Type propType, string propName, object value)
        {
            if (attributeTypes.Contains(propType))
                return GenerateXmlFromPrimitive(propName, value);
            if (propType.Equals(typeof(object)))
                return GenerateXmlFromObject(propName, value);
            if (typeof(Expression).IsAssignableFrom(propType))
                return GenerateXmlFromExpression(propName, value as Expression);
            if (value is MethodInfo || propType.Equals(typeof(MethodInfo)))
                return GenerateXmlFromMethodInfo(propName, value as MethodInfo);
            if (value is PropertyInfo || propType.Equals(typeof(PropertyInfo)))
                return GenerateXmlFromPropertyInfo(propName, value as PropertyInfo);
            if (value is FieldInfo || propType.Equals(typeof(FieldInfo)))
                return GenerateXmlFromFieldInfo(propName, value as FieldInfo);
            if (value is ConstructorInfo || propType.Equals(typeof(ConstructorInfo)))
                return GenerateXmlFromConstructorInfo(propName, value as ConstructorInfo);
            if (propType.Equals(typeof(Type)))
                return GenerateXmlFromType(propName, value as Type);
            if (IsIEnumerableOf<Expression>(propType))
                return GenerateXmlFromExpressionList(propName, AsIEnumerableOf<Expression>(value));
            if (IsIEnumerableOf<MemberInfo>(propType))
                return GenerateXmlFromMemberInfoList(propName, AsIEnumerableOf<MemberInfo>(value));
            if (IsIEnumerableOf<ElementInit>(propType))
                return GenerateXmlFromElementInitList(propName, AsIEnumerableOf<ElementInit>(value));
            if (IsIEnumerableOf<MemberBinding>(propType))
                return GenerateXmlFromBindingList(propName, AsIEnumerableOf<MemberBinding>(value));
            throw new NotSupportedException(propName);
        }

        private object GenerateXmlFromObject(string propName, object value)
        {
            object result = null;
            if (value is Type)
                result = GenerateXmlFromTypeCore((Type)value);
            if (result == null)
                result = value.ToString();
            return new XElement(propName,
                result);
        }

        private bool IsIEnumerableOf<T>(Type propType)
        {
            if (!propType.IsGenericType)
                return false;
            Type[] typeArgs = propType.GetGenericArguments();
            if (typeArgs.Length != 1)
                return false;
            if (!typeof(T).IsAssignableFrom(typeArgs[0]))
                return false;
            if (!typeof(IEnumerable<>).MakeGenericType(typeArgs).IsAssignableFrom(propType))
                return false;
            return true;
        }

        private IEnumerable<T> AsIEnumerableOf<T>(object value)
        {
            if (value == null)
                return null;
            return (value as IEnumerable).Cast<T>();
        }

        private object GenerateXmlFromElementInitList(string propName, IEnumerable<ElementInit> initializers)
        {
            if (initializers == null)
                initializers = new ElementInit[] { };
            return new XElement(propName,
                from elementInit in initializers
                select GenerateXmlFromElementInitializer(elementInit));
        }

        private object GenerateXmlFromElementInitializer(ElementInit elementInit)
        {
            return new XElement("ElementInit",
                GenerateXmlFromMethodInfo("AddMethod", elementInit.AddMethod),
                GenerateXmlFromExpressionList("Arguments", elementInit.Arguments));
        }

        private object GenerateXmlFromExpressionList(string propName, IEnumerable<Expression> expressions)
        {
            return new XElement(propName,
                    from expression in expressions
                    select GenerateXmlFromExpressionCore(expression));
        }

        private object GenerateXmlFromMemberInfoList(string propName, IEnumerable<MemberInfo> members)
        {
            if (members == null)
                members = new MemberInfo[] { };
            return new XElement(propName,
                   from member in members
                   select GenerateXmlFromProperty(member.GetType(), "Info", member));
        }

        private object GenerateXmlFromBindingList(string propName, IEnumerable<MemberBinding> bindings)
        {
            if (bindings == null)
                bindings = new MemberBinding[] { };
            return new XElement(propName,
                from binding in bindings
                select GenerateXmlFromBinding(binding));
        }

        private object GenerateXmlFromBinding(MemberBinding binding)
        {
            switch (binding.BindingType)
            {
                case MemberBindingType.Assignment:
                    return GenerateXmlFromAssignment(binding as MemberAssignment);
                case MemberBindingType.ListBinding:
                    return GenerateXmlFromListBinding(binding as MemberListBinding);
                case MemberBindingType.MemberBinding:
                    return GenerateXmlFromMemberBinding(binding as MemberMemberBinding);
                default:
                    throw new NotSupportedException(string.Format("Binding type {0} not supported.", binding.BindingType));
            }
        }

        private object GenerateXmlFromMemberBinding(MemberMemberBinding memberMemberBinding)
        {
            return new XElement("MemberMemberBinding",
                GenerateXmlFromProperty(memberMemberBinding.Member.GetType(), "Member", memberMemberBinding.Member),
                GenerateXmlFromBindingList("Bindings", memberMemberBinding.Bindings));
        }


        private object GenerateXmlFromListBinding(MemberListBinding memberListBinding)
        {
            return new XElement("MemberListBinding",
                GenerateXmlFromProperty(memberListBinding.Member.GetType(), "Member", memberListBinding.Member),
                GenerateXmlFromProperty(memberListBinding.Initializers.GetType(), "Initializers", memberListBinding.Initializers));
        }

        private object GenerateXmlFromAssignment(MemberAssignment memberAssignment)
        {
            return new XElement("MemberAssignment",
                GenerateXmlFromProperty(memberAssignment.Member.GetType(), "Member", memberAssignment.Member),
                GenerateXmlFromProperty(memberAssignment.Expression.GetType(), "Expression", memberAssignment.Expression));
        }

        private XElement GenerateXmlFromExpression(string propName, Expression e)
        {
            return new XElement(propName, GenerateXmlFromExpressionCore(e));
        }

        private object GenerateXmlFromType(string propName, Type type)
        {
            return new XElement(propName, GenerateXmlFromTypeCore(type));
        }

        private XElement GenerateXmlFromTypeCore(Type type)
        {
            //vsadov: add detection of VB anon types
            if (type.Name.StartsWith("<>f__") || type.Name.StartsWith("VB$AnonymousType"))
                return new XElement("AnonymousType",
                    new XAttribute("Name", type.FullName),
                    from property in type.GetProperties()
                    select new XElement("Property",
                        new XAttribute("Name", property.Name),
                        GenerateXmlFromTypeCore(property.PropertyType)),
                    new XElement("Constructor",
                            from parameter in type.GetConstructors().First().GetParameters()
                            select new XElement("Parameter",
                                new XAttribute("Name", parameter.Name),
                                GenerateXmlFromTypeCore(parameter.ParameterType))
                    ));

            else        
            {
                //vsadov: GetGenericArguments returns args for nongeneric types 
                //like arrays no need to save them.
                if (type.IsGenericType)   
                {
                    return new XElement("Type",
                                            new XAttribute("Name", type.GetGenericTypeDefinition().FullName),
                                            from genArgType in type.GetGenericArguments()
                                            select GenerateXmlFromTypeCore(genArgType));
                }
                else
                {
                    return new XElement("Type",new XAttribute("Name", type.FullName));
                }

            }
        }

        private object GenerateXmlFromPrimitive(string propName, object value)
        {
            return new XAttribute(propName, value ?? string.Empty);
        }

        private object GenerateXmlFromMethodInfo(string propName, MethodInfo methodInfo)
        {
            if (methodInfo == null)
                return new XElement(propName);
            return new XElement(propName,
                        new XAttribute("MemberType", methodInfo.MemberType),
                        new XAttribute("MethodName", methodInfo.Name),
                        GenerateXmlFromType("DeclaringType", methodInfo.DeclaringType),
                        new XElement("Parameters",
                            from param in methodInfo.GetParameters()
                            select GenerateXmlFromType("Type", param.ParameterType)),
                        new XElement("GenericArgTypes",
                            from argType in methodInfo.GetGenericArguments()
                            select GenerateXmlFromType("Type", argType)));
        }

        private object GenerateXmlFromPropertyInfo(string propName, PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                return new XElement(propName);
            return new XElement(propName,
                        new XAttribute("MemberType", propertyInfo.MemberType),
                        new XAttribute("PropertyName", propertyInfo.Name),
                        GenerateXmlFromType("DeclaringType", propertyInfo.DeclaringType),
                        new XElement("IndexParameters",
                            from param in propertyInfo.GetIndexParameters()
                            select GenerateXmlFromType("Type", param.ParameterType)));
        }

        private object GenerateXmlFromFieldInfo(string propName, FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
                return new XElement(propName);
            return new XElement(propName,
                        new XAttribute("MemberType", fieldInfo.MemberType),
                        new XAttribute("FieldName", fieldInfo.Name),
                        GenerateXmlFromType("DeclaringType", fieldInfo.DeclaringType));
        }

        private object GenerateXmlFromConstructorInfo(string propName, ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null)
                return new XElement(propName);
            return new XElement(propName,
                        new XAttribute("MemberType", constructorInfo.MemberType),
                        new XAttribute("MethodName", constructorInfo.Name),
                        GenerateXmlFromType("DeclaringType", constructorInfo.DeclaringType),
                        new XElement("Parameters",
                            from param in constructorInfo.GetParameters()
                            select new XElement("Parameter",
                                new XAttribute("Name", param.Name),
                                GenerateXmlFromType("Type", param.ParameterType))));
        }


        /*
         * DESERIALIZATION 
         */


        public Expression Deserialize(XElement xml)
        {
            parameters.Clear();
            return ParseExpressionFromXmlNonNull(xml);
        }

        public Expression<TDelegate> Deserialize<TDelegate>(XElement xml)
        {
            Expression e = Deserialize(xml);
            if (e is Expression<TDelegate>)
                return e as Expression<TDelegate>;
            throw new Exception("xml must represent an Expression<TDelegate>");
        }

        private Expression ParseExpressionFromXml(XElement xml)
        {
            if (xml.IsEmpty)
                return null;

            return ParseExpressionFromXmlNonNull(xml.Elements().First());
        }

        private Expression ParseExpressionFromXmlNonNull(XElement xml)
        {
            Expression expression = ApplyCustomDeserializers(xml);
            if (expression != null)
                return expression;
            switch (xml.Name.LocalName)
            {
                case "BinaryExpression":
                    return ParseBinaryExpresssionFromXml(xml);
                case "ConstantExpression":
                    return ParseConstatExpressionFromXml(xml);
                case "ParameterExpression":
                    return ParseParameterExpressionFromXml(xml);
                case "LambdaExpression":
                    return ParseLambdaExpressionFromXml(xml);
                case "MethodCallExpression":
                    return ParseMethodCallExpressionFromXml(xml);
                case "UnaryExpression":
                    return ParseUnaryExpressionFromXml(xml);
                case "MemberExpression":
                    return ParseMemberExpressionFromXml(xml);
                case "NewExpression":
                    return ParseNewExpressionFromXml(xml);
                case "ListInitExpression":
                    return ParseListInitExpressionFromXml(xml);
                case "MemberInitExpression":
                    return ParseMemberInitExpressionFromXml(xml);
                case "ConditionalExpression":
                    return ParseConditionalExpressionFromXml(xml);
                case "NewArrayExpression":
                    return ParseNewArrayExpressionFromXml(xml);
                case "TypeBinaryExpression":
                    return ParseTypeBinaryExpressionFromXml(xml);
                case "InvocationExpression":
                    return ParseInvocationExpressionFromXml(xml);
                default:
                    throw new NotSupportedException(xml.Name.LocalName);
            }
        }

        private Expression ApplyCustomDeserializers(XElement xml)
        {
            foreach (var converter in Converters)
            {
                Expression result = converter.Deserialize(xml);
                if (result != null) 
                    return result;
            }
            return null;
        }

        private Expression ParseInvocationExpressionFromXml(XElement xml)
        {
            Expression expression = ParseExpressionFromXml(xml.Element("Expression"));
            var arguments = ParseExpressionListFromXml<Expression>(xml, "Arguments");
            return Expression.Invoke(expression, arguments);
        }

        private Expression ParseTypeBinaryExpressionFromXml(XElement xml)
        {
            Expression expression = ParseExpressionFromXml(xml.Element("Expression"));
            Type typeOperand = ParseTypeFromXml(xml.Element("TypeOperand"));
            return Expression.TypeIs(expression, typeOperand);
        }

        private Expression ParseNewArrayExpressionFromXml(XElement xml)
        {
            Type type = ParseTypeFromXml(xml.Element("Type"));
            if (!type.IsArray)
                throw new Exception("Expected array type");
            Type elemType = type.GetElementType();
            var expressions = ParseExpressionListFromXml<Expression>(xml, "Expressions");
            switch (xml.Attribute("NodeType").Value)
            {
                case "NewArrayInit":
                    return Expression.NewArrayInit(elemType, expressions);
                case "NewArrayBounds":
                    return Expression.NewArrayBounds(elemType, expressions);
                default:
                    throw new Exception("Expected NewArrayInit or NewArrayBounds");
            }
        }

        private Expression ParseConditionalExpressionFromXml(XElement xml)
        {
            Expression test = ParseExpressionFromXml(xml.Element("Test"));
            Expression ifTrue = ParseExpressionFromXml(xml.Element("IfTrue"));
            Expression ifFalse = ParseExpressionFromXml(xml.Element("IfFalse"));
            return Expression.Condition(test, ifTrue, ifFalse);
        }

        private Expression ParseMemberInitExpressionFromXml(XElement xml)
        {
            NewExpression newExpression = ParseNewExpressionFromXml(xml.Element("NewExpression").Element("NewExpression")) as NewExpression;
            var bindings = ParseBindingListFromXml(xml, "Bindings").ToArray();
            return Expression.MemberInit(newExpression, bindings);
        }



        private Expression ParseListInitExpressionFromXml(XElement xml)
        {
            NewExpression newExpression = ParseExpressionFromXml(xml.Element("NewExpression")) as NewExpression;
            if (newExpression == null) throw new Exception("Expceted a NewExpression");
            var initializers = ParseElementInitListFromXml(xml, "Initializers").ToArray();
            return Expression.ListInit(newExpression, initializers);
        }

        private Expression ParseNewExpressionFromXml(XElement xml)
        {
            ConstructorInfo constructor = ParseConstructorInfoFromXml(xml.Element("Constructor"));
            var arguments = ParseExpressionListFromXml<Expression>(xml, "Arguments").ToArray();
            var members = ParseMemberInfoListFromXml<MemberInfo>(xml, "Members").ToArray();
            if (members.Length == 0)
                return Expression.New(constructor, arguments);
            return Expression.New(constructor, arguments, members);
        }

        private Expression ParseMemberExpressionFromXml(XElement xml)
        {
            Expression expression = ParseExpressionFromXml(xml.Element("Expression"));
            MemberInfo member = ParseMemberInfoFromXml(xml.Element("Member"));
            return Expression.MakeMemberAccess(expression, member);
        }

        private MemberInfo ParseMemberInfoFromXml(XElement xml)
        {
            MemberTypes memberType = (MemberTypes)ParseConstantFromAttribute<MemberTypes>(xml, "MemberType");
            switch (memberType)
            {
                case MemberTypes.Field:
                    return ParseFieldInfoFromXml(xml);
                case MemberTypes.Property:
                    return ParsePropertyInfoFromXml(xml);
                case MemberTypes.Method:
                    return ParseMethodInfoFromXml(xml);
                case MemberTypes.Constructor:
                    return ParseConstructorInfoFromXml(xml);
                case MemberTypes.Custom:
                case MemberTypes.Event:
                case MemberTypes.NestedType:
                case MemberTypes.TypeInfo:
                default:
                    throw new NotSupportedException(string.Format("MEmberType {0} not supported", memberType));
            }

        }

        private MemberInfo ParseFieldInfoFromXml(XElement xml)
        {
            string fieldName = (string)ParseConstantFromAttribute<string>(xml, "FieldName");
            Type declaringType = ParseTypeFromXml(xml.Element("DeclaringType"));
            return declaringType.GetField(fieldName);
        }

        private MemberInfo ParsePropertyInfoFromXml(XElement xml)
        {
            string propertyName = (string)ParseConstantFromAttribute<string>(xml, "PropertyName");
            Type declaringType = ParseTypeFromXml(xml.Element("DeclaringType"));
            var ps = from paramXml in xml.Element("IndexParameters").Elements()
                     select ParseTypeFromXml(paramXml);
            return declaringType.GetProperty(propertyName, ps.ToArray());
        }

        private Expression ParseUnaryExpressionFromXml(XElement xml)
        {
            Expression operand = ParseExpressionFromXml(xml.Element("Operand"));
            MethodInfo method = ParseMethodInfoFromXml(xml.Element("Method"));
            var isLifted = (bool)ParseConstantFromAttribute<bool>(xml, "IsLifted");
            var isLiftedToNull = (bool)ParseConstantFromAttribute<bool>(xml, "IsLiftedToNull");
            var expressionType = (ExpressionType)ParseConstantFromAttribute<ExpressionType>(xml, "NodeType");
            var type = ParseTypeFromXml(xml.Element("Type"));
            // TODO: Why can't we use IsLifted and IsLiftedToNull here?  
            // May need to special case a nodeType if it needs them.
            return Expression.MakeUnary(expressionType, operand, type, method);
        }

        private Expression ParseMethodCallExpressionFromXml(XElement xml)
        {
            Expression instance = ParseExpressionFromXml(xml.Element("Object"));
            MethodInfo method = ParseMethodInfoFromXml(xml.Element("Method"));
            var arguments = ParseExpressionListFromXml<Expression>(xml, "Arguments").ToArray();
            return Expression.Call(instance, method, arguments);
        }

        private Expression ParseLambdaExpressionFromXml(XElement xml)
        {
            var body = ParseExpressionFromXml(xml.Element("Body"));
            var parameters = ParseExpressionListFromXml<ParameterExpression>(xml, "Parameters");
            var type = ParseTypeFromXml(xml.Element("Type"));
            // We may need to 
            //var lambdaExpressionReturnType = type.GetMethod("Invoke").ReturnType;
            //if (lambdaExpressionReturnType.IsArray)
            //{
                
            //    type = typeof(IEnumerable<>).MakeGenericType(type.GetElementType());
            //}
            return Expression.Lambda(type, body, parameters);
        }

        private IEnumerable<T> ParseExpressionListFromXml<T>(XElement xml, string elemName) where T : Expression
        {
            return from tXml in xml.Element(elemName).Elements()
                   select (T)ParseExpressionFromXmlNonNull(tXml);
        }

        private IEnumerable<T> ParseMemberInfoListFromXml<T>(XElement xml, string elemName) where T : MemberInfo
        {
            return from tXml in xml.Element(elemName).Elements()
                   select (T)ParseMemberInfoFromXml(tXml);
        }

        private IEnumerable<ElementInit> ParseElementInitListFromXml(XElement xml, string elemName)
        {
            return from tXml in xml.Element(elemName).Elements()
                   select ParseElementInitFromXml(tXml);
        }

        private ElementInit ParseElementInitFromXml(XElement xml)
        {
            MethodInfo addMethod = ParseMethodInfoFromXml(xml.Element("AddMethod"));
            var arguments = ParseExpressionListFromXml<Expression>(xml, "Arguments");
            return Expression.ElementInit(addMethod, arguments);

        }

        private IEnumerable<MemberBinding> ParseBindingListFromXml(XElement xml, string elemName)
        {
            return from tXml in xml.Element(elemName).Elements()
                   select ParseBindingFromXml(tXml);
        }

        private MemberBinding ParseBindingFromXml(XElement tXml)
        {
            MemberInfo member = ParseMemberInfoFromXml(tXml.Element("Member"));
            switch (tXml.Name.LocalName)
            {
                case "MemberAssignment":
                    Expression expression = ParseExpressionFromXml(tXml.Element("Expression"));
                    return Expression.Bind(member, expression);
                case "MemberMemberBinding":
                    var bindings = ParseBindingListFromXml(tXml, "Bindings");
                    return Expression.MemberBind(member, bindings);
                case "MemberListBinding":
                    var initializers = ParseElementInitListFromXml(tXml, "Initializers");
                    return Expression.ListBind(member, initializers);
            }
            throw new NotImplementedException();
        }


        private Expression ParseParameterExpressionFromXml(XElement xml)
        {
            Type type = ParseTypeFromXml(xml.Element("Type"));
            string name = (string)ParseConstantFromAttribute<string>(xml, "Name");
            //vs: hack
            string id = name + type.FullName;
            if (!parameters.ContainsKey(id))
                parameters.Add(id, Expression.Parameter(type, name));
            return parameters[id];
        }

        private Expression ParseConstatExpressionFromXml(XElement xml)
        {
            Type type = ParseTypeFromXml(xml.Element("Type"));
            return Expression.Constant(ParseConstantFromElement(xml, "Value", type), type);
        }

        private Type ParseTypeFromXml(XElement xml)
        {
            Debug.Assert(xml.Elements().Count() == 1);
            return ParseTypeFromXmlCore(xml.Elements().First());
        }

        private Type ParseTypeFromXmlCore(XElement xml)
        {
            switch (xml.Name.ToString())
            {
                case "Type":
                    return ParseNormalTypeFromXmlCore(xml);
                case "AnonymousType":
                    return ParseAnonymousTypeFromXmlCore(xml);
                default:
                    throw new ArgumentException("Expected 'Type' or 'AnonymousType'");
            }

        }

        private Type ParseNormalTypeFromXmlCore(XElement xml)
        {
            if (!xml.HasElements)
                return resolver.GetType(xml.Attribute("Name").Value);

            var genericArgumentTypes = from genArgXml in xml.Elements()
                                       select ParseTypeFromXmlCore(genArgXml);
            return resolver.GetType(xml.Attribute("Name").Value, genericArgumentTypes);
        }

        private Type ParseAnonymousTypeFromXmlCore(XElement xElement)
        {
            string name = xElement.Attribute("Name").Value;
            var properties = from propXml in xElement.Elements("Property")
                             select new ExpressionSerializationTypeResolver.NameTypePair
                             {
                                 Name = propXml.Attribute("Name").Value,
                                 Type = ParseTypeFromXml(propXml)
                             };
            var ctr_params = from propXml in xElement.Elements("Constructor").Elements("Parameter")
                             select new ExpressionSerializationTypeResolver.NameTypePair
                             {
                                 Name = propXml.Attribute("Name").Value,
                                 Type = ParseTypeFromXml(propXml)
                             };

            return resolver.GetOrCreateAnonymousTypeFor(name, properties.ToArray(), ctr_params.ToArray());
        }

        private Expression ParseBinaryExpresssionFromXml(XElement xml)
        {
            var expressionType = (ExpressionType)ParseConstantFromAttribute<ExpressionType>(xml, "NodeType"); ;
            var left = ParseExpressionFromXml(xml.Element("Left"));
            var right = ParseExpressionFromXml(xml.Element("Right"));
            var isLifted = (bool)ParseConstantFromAttribute<bool>(xml, "IsLifted");
            var isLiftedToNull = (bool)ParseConstantFromAttribute<bool>(xml, "IsLiftedToNull");
            var type = ParseTypeFromXml(xml.Element("Type"));
            var method = ParseMethodInfoFromXml(xml.Element("Method"));
            LambdaExpression conversion = ParseExpressionFromXml(xml.Element("Conversion")) as LambdaExpression;
            if (expressionType == ExpressionType.Coalesce)
                return Expression.Coalesce(left, right, conversion);
            return Expression.MakeBinary(expressionType, left, right, isLiftedToNull, method);
        }

        private MethodInfo ParseMethodInfoFromXml(XElement xml)
        {
            if (xml.IsEmpty)
                return null;
            string name = (string)ParseConstantFromAttribute<string>(xml, "MethodName");
            Type declaringType = ParseTypeFromXml(xml.Element("DeclaringType"));
            var ps = from paramXml in xml.Element("Parameters").Elements()
                     select ParseTypeFromXml(paramXml);
            var genArgs = from argXml in xml.Element("GenericArgTypes").Elements()
                          select ParseTypeFromXml(argXml);
            return resolver.GetMethod(declaringType, name, ps.ToArray(), genArgs.ToArray());
        }

        private ConstructorInfo ParseConstructorInfoFromXml(XElement xml)
        {
            if (xml.IsEmpty)
                return null;
            Type declaringType = ParseTypeFromXml(xml.Element("DeclaringType"));
            var ps = from paramXml in xml.Element("Parameters").Elements()
                     select ParseParameterFromXml(paramXml);
            ConstructorInfo ci = declaringType.GetConstructor(ps.ToArray());
            return ci;
        }

        private Type ParseParameterFromXml(XElement xml)
        {
            string name = (string)ParseConstantFromAttribute<string>(xml, "Name");
            Type type = ParseTypeFromXml(xml.Element("Type"));
            return type;

        }

        private object ParseConstantFromAttribute<T>(XElement xml, string attrName)
        {
            string objectStringValue = xml.Attribute(attrName).Value;
            if (typeof(Type).IsAssignableFrom(typeof(T)))
                throw new Exception("We should never be encoding Types in attributes now.");
            if (typeof(Enum).IsAssignableFrom(typeof(T)))
                return Enum.Parse(typeof(T), objectStringValue);
            return Convert.ChangeType(objectStringValue, typeof(T));
        }

        private object ParseConstantFromAttribute(XElement xml, string attrName, Type type)
        {
            string objectStringValue = xml.Attribute(attrName).Value;
            if (typeof(Type).IsAssignableFrom(type))
                throw new Exception("We should never be encoding Types in attributes now.");
            if (typeof(Enum).IsAssignableFrom(type))
                return Enum.Parse(type, objectStringValue);
            return Convert.ChangeType(objectStringValue, type);
        }

        private object ParseConstantFromElement(XElement xml, string elemName, Type type)
        {
            string objectStringValue = xml.Element(elemName).Value;
            if (typeof(Type).IsAssignableFrom(type))
                return ParseTypeFromXml(xml.Element("Value"));
            if (typeof(Enum).IsAssignableFrom(type))
                return Enum.Parse(type, objectStringValue);
            return Convert.ChangeType(objectStringValue, type);
        }


		public static string ToGenericTypeFullNameString(Type t)
		{
			if (t.FullName == null && t.IsGenericParameter)
				return t.GenericParameterPosition == 0 ? "T" : "T" + t.GenericParameterPosition;

			if (!t.IsGenericType)
				return t.FullName;

			string value = t.FullName.Substring(0, t.FullName.IndexOf('`')) + "<";
			Type[] genericArgs = t.GetGenericArguments();
			List<string> list = new List<string>();
			for (int i = 0; i < genericArgs.Length; i++)
			{
				value += "{" + i + "},";
				string s = ToGenericTypeFullNameString(genericArgs[i]);
				list.Add(s);
			}
			value = value.TrimEnd(',');
			value += ">";
			value = string.Format(value, list.ToArray<string>());
			return value;

		}

		public static string ToGenericTypeNameString(Type t)
		{
			string fullname = ToGenericTypeFullNameString(t);
			fullname = fullname.Substring(fullname.LastIndexOf('.') + 1).TrimEnd('>');
			return fullname;
		}

    }
}
