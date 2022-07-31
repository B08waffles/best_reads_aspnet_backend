using System;
using JetBrains.Annotations;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Microsoft.EntityFrameworkCore;

/// <summary>
/// Represents a <see cref="T:Microsoft.EntityFrameworkCore.ServerVersion" /> for MySQL database servers.
/// For MariaDB database servers, use <see cref="T:Microsoft.EntityFrameworkCore.MariaDbServerVersion" /> instead.
/// </summary>
public class MySqlServerVersion : ServerVersion
{
	public class MySqlServerVersionSupport : ServerVersionSupport
	{
		public override bool DateTimeCurrentTimestamp => ServerVersion.Version >= new Version(5, 6, 5);

		public override bool DateTime6 => ServerVersion.Version >= new Version(5, 6, 4);

		public override bool LargerKeyLength => ServerVersion.Version >= new Version(5, 7, 7);

		public override bool RenameIndex => ServerVersion.Version >= new Version(5, 7, 0);

		public override bool RenameColumn => ServerVersion.Version >= new Version(8, 0, 0);

		public override bool WindowFunctions => ServerVersion.Version >= new Version(8, 0, 0);

		public override bool FloatCast => false;

		public override bool DoubleCast => ServerVersion.Version >= new Version(8, 0, 17);

		public override bool OuterApply => ServerVersion.Version >= new Version(8, 0, 14);

		public override bool CrossApply => ServerVersion.Version >= new Version(8, 0, 14);

		public override bool OuterReferenceInMultiLevelSubquery => ServerVersion.Version >= new Version(8, 0, 14);

		public override bool Json => ServerVersion.Version >= new Version(5, 7, 8);

		public override bool GeneratedColumns => ServerVersion.Version >= new Version(5, 7, 6);

		public override bool NullableGeneratedColumns => ServerVersion.Version >= new Version(5, 7, 0);

		public override bool ParenthesisEnclosedGeneratedColumnExpressions => GeneratedColumns;

		public override bool DefaultCharSetUtf8Mb4 => ServerVersion.Version >= new Version(8, 0, 0);

		public override bool DefaultExpression => ServerVersion.Version >= new Version(8, 0, 13);

		public override bool AlternativeDefaultExpression => false;

		public override bool SpatialIndexes => ServerVersion.Version >= new Version(5, 7, 5);

		public override bool SpatialReferenceSystemRestrictedColumns => ServerVersion.Version >= new Version(8, 0, 3);

		public override bool SpatialFunctionAdditions => false;

		public override bool SpatialSupportFunctionAdditions => ServerVersion.Version >= new Version(5, 7, 6);

		public override bool SpatialSetSridFunction => ServerVersion.Version >= new Version(8, 0, 0);

		public override bool SpatialDistanceFunctionImplementsAndoyer => ServerVersion.Version >= new Version(8, 0, 0);

		public override bool SpatialDistanceSphereFunction => ServerVersion.Version >= new Version(8, 0, 0);

		public override bool SpatialGeographic => ServerVersion.Version >= new Version(8, 0, 0);

		public override bool ExceptIntercept => false;

		public override bool ExceptInterceptPrecedence => false;

		public override bool JsonDataTypeEmulation => false;

		public override bool ImplicitBoolCheckUsesIndex => ServerVersion.Version >= new Version(8, 0, 0);

		public override bool MySqlBug96947Workaround
		{
			get
			{
				if (ServerVersion.Version >= new Version(5, 7, 0))
				{
					return ServerVersion.Version < new Version(8, 0, 25);
				}
				return false;
			}
		}

		public override bool MySqlBug104294Workaround => ServerVersion.Version >= new Version(8, 0, 0);

		public override bool FullTextParser => ServerVersion.Version >= new Version(5, 7, 3);

		public override bool InformationSchemaCheckConstraintsTable => ServerVersion.Version >= new Version(8, 0, 16);

		public override bool MySqlBugLimit0Offset0ExistsWorkaround => true;

		internal MySqlServerVersionSupport([NotNull] ServerVersion serverVersion)
			: base(serverVersion)
		{
		}
	}

	public static readonly string MySqlTypeIdentifier = "MySql".ToLowerInvariant();

	public static readonly ServerVersion LatestSupportedServerVersion = new MySqlServerVersion(new Version(8, 0, 21));

	public override ServerVersionSupport Supports { get; }

	public MySqlServerVersion(Version version)
		: base(version, ServerType.MySql)
	{
		Supports = new MySqlServerVersionSupport(this);
	}

	public MySqlServerVersion(string versionString)
		: this(ServerVersion.Parse(versionString, ServerType.MySql))
	{
	}

	public MySqlServerVersion(ServerVersion serverVersion)
		: base(serverVersion.Version, serverVersion.Type, serverVersion.TypeIdentifier)
	{
		if (Type != 0 || !string.Equals(TypeIdentifier, MySqlTypeIdentifier))
		{
			throw new ArgumentException("MySqlServerVersion is not compatible with the supplied server type.");
		}
		Supports = new MySqlServerVersionSupport(this);
	}
}
