# SeparateDocs: XMLドキュメントコメント分割スクリプト

## 多言語 XML ドキュメントコメントのサンプルプロジェクト

自作の PowerShell スクリプトを使って、C# の XML ドキュメントコメントを多言語対応したプロジェクトのサンプルです。

## やったこと

1. 空の C# プロジェクトを作り、XML ドキュメントコメントを XML ファイルに出力するように設定した。
    * プロパティ → ビルド → 出力 →「XMLドキュメントファイル」をチェックON
1. XML ドキュメントコメントを持つサンプル C# コードを書いた。
1. `xml:lang` 属性を使って XML ドキュメントコメントを書き直し、多言語化した。
    * 下記参照
1. プロジェクトのビルド後に PowerShell スクリプト（`SeparateDocs.ps1`）を実行し、多言語化した XML ドキュメントコメントから言語別の XML ファイルを生成するようにした。
    * プロジェクトの「ビルド後イベント」にコマンドラインを記載。
1. 作成する NuGet パッケージに言語別の XML ファイルを含めるよう `.csproj` を修正した。
    * `<Content>` タグを追加。

## 多言語化された XML ドキュメントコメントの例

```CSharp
using System;
using System.Runtime.InteropServices;

namespace SampleLibrary
{
    /// <summary>
    ///     <para xml:lang="en">Converts an array to an array of a different type.</para>
    ///     <para xml:lang="ja">配列を異なる型の配列に変換します。</para>
    /// </summary>
    public class ArrayConverter
    {
        /// <summary>
        ///     <para xml:lang="en">Converts float[] to byte[].</para>
        ///     <para xml:lang="ja">float[] を byte[] に変換します。</para>
        /// </summary>
        /// <param name="source">
        ///     <para xml:lang="en">An array of floats.</para>
        ///     <para xml:lang="ja">float 配列。</para>
        /// </param>
        /// <returns>
        ///     <para xml:lang="en">A new array of bytes.</para>
        ///     <para xml:lang="ja">新しい byte 配列。</para>
        /// </returns>
        public static byte[] ToByteArray( float[] source )
        {
            var sourceFloatSpan = new Span<float>( source );
            var sourceByteSpan = MemoryMarshal.Cast<float, byte>( sourceFloatSpan );
            return sourceByteSpan.ToArray();
        }
    }
}
```

## ビルド後イベントのコマンドライン

```
powershell -ExecutionPolicy RemoteSigned -File "$(ProjectDir)separateDocs.ps1" $(ProjectName) $(OutDir) $(ProjectDir)Generated
```

## ビルド結果

SampleLibrary プロジェクトをビルドすると、プロジェクトディレクトリに次のような内容の `Generated\` フォルダが作成されます。
```
Generated\
  ja\
    SampleLibrary.xml  ... 日本語用 XML ドキュメントコメントファイル
  SampleLibrary.xml    ... 既定（英語）用 XML ドキュメントコメントファイル
```

## パック結果

`SampleLibrary.csproj` に下記を追加。

```xml
<ItemGroup>
  <Content Include="Generated\*\*.xml" Pack="true" PackagePath="lib\$(TargetFramework)" />
</ItemGroup>
```

これで、SampleLibrary プロジェクトを「パック」して出力される `SampleLibrary.1.0.0.nupkg` ファイルに、`.dll` と共に言語別 XML ドキュメントコメントファイルが含まれるようになります。

```
lib\
  net5.0\
    ja\
      SampleLibrary.xml
    SampleLibrary.dll
    SampleLibrary.xml
```
