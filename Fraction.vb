Imports Extension = System.Runtime.CompilerServices.ExtensionAttribute
Imports Method = System.Runtime.CompilerServices.MethodImplAttribute

Imports bool = System.Boolean

Namespace EXS

    Partial Public Module Exten
        '''<summary>Convert input into fraction format.</summary>
        <Extension, Method(inline)>
        Public Function f(Input As int) As Math.fraction
            Return Input
        End Function
        '''<summary>Convert input into fraction format.</summary>
        <Extension, Method(inline)>
        Public Function f(Input As (int, int)) As Math.fraction
            Return Input
        End Function

        '''<summary>Convert input into fraction format.</summary>
        <Extension, Method(inline)>
        Public Function f(Input As Int32) As Math.fraction
            Return Input
        End Function
        '''<summary>Convert input into fraction format.</summary>
        <Extension, Method(inline)>
        Public Function f(Input As (Int32, Int32)) As Math.fraction
            Return Input
        End Function
    End Module

    Namespace Math

        Partial Public Module Exten

            Private _gcd As Func(Of int, int, int) = "la.1 t:0 la.0 ; :0 la.1.0.1 % re use-me".compile(Of Func(Of int, int, int)).fin
            <Method(inline)>
            Public Function gcd(A As int, B As int) As int
                Return _gcd(A, B)
            End Function
            <Method(inline)>
            Public Function lmc(A As int, B As int) As int
                Return A * B / _gcd(A, B)
            End Function
        End Module

        Public Structure fraction
            Public ReadOnly top, low As int
            Sub New(Input As (top As int, low As int))
                Dim N = gcd(top, low)
                top = Input.top / N
                low = Input.low / N
            End Sub
            Sub New(Top As int, Low As int)
                Dim N = gcd(Top, Low)
                Me.top = Top / N
                Me.low = Low / N
            End Sub

            <Method(inline)>
            Public Shared Operator *(A As fraction, B As int) As fraction
                Return (A.top * B, A.low)
            End Operator
            <Method(inline)>
            Public Shared Operator /(A As fraction, B As int) As fraction
                Return (A.top, A.low * B)
            End Operator
            <Method(inline)>
            Public Shared Operator +(A As fraction, B As int) As fraction
                Return (A.top + B * A.low, A.low)
            End Operator
            <Method(inline)>
            Public Shared Operator -(A As fraction, B As int) As fraction
                Return (A.top - B * A.low, A.low)
            End Operator

            <Method(inline)>
            Public Shared Operator *(A As fraction, B As fraction) As fraction
                Return (A.top * B.top, A.low * B.low)
            End Operator
            <Method(inline)>
            Public Shared Operator /(A As fraction, B As fraction) As fraction
                Return (A.top / B.top, A.low / B.low)
            End Operator
            <Method(inline)>
            Public Shared Operator +(A As fraction, B As fraction) As fraction
                Return If(A.low = B.low, (A.top + B.top, A.low), (A.top * B.low + B.top * A.low, A.low * B.low))
            End Operator
            <Method(inline)>
            Public Shared Operator -(A As fraction, B As fraction) As fraction
                Return If(A.low = B.low, (A.top - B.top, A.low), (A.top * B.low - B.top * A.low, A.low * B.low))
            End Operator

            <Method(inline)>
            Public Shared Operator >(A As fraction, B As fraction) As bool
                Return A.top * B.low > B.top * A.low
            End Operator
            <Method(inline)>
            Public Shared Operator <(A As fraction, B As fraction) As bool
                Return A.top * B.low < B.top * A.low
            End Operator
            <Method(inline)>
            Public Shared Operator =(A As fraction, B As fraction) As bool
                Return A.low = B.low And A.top = B.top
            End Operator
            <Method(inline)>
            Public Shared Operator <>(A As fraction, B As fraction) As bool
                Return Not (A = B)
            End Operator
            <Method(inline)>
            Public Shared Operator >=(A As fraction, B As fraction) As bool
                Return Not A < B
            End Operator
            <Method(inline)>
            Public Shared Operator <=(A As fraction, B As fraction) As bool
                Return Not A > B
            End Operator

            <Method(inline)>
            Public Shared Widening Operator CType(Input As int) As fraction
                Return New fraction((Input, 1))
            End Operator
            <Method(inline)>
            Public Shared Widening Operator CType(Input As (int, int)) As fraction
                Return New fraction(Input)
            End Operator
            <Method(inline)>
            Public Shared Widening Operator CType(Input As (Int32, Int32)) As fraction
                Return New fraction(Input.Item1, Input.Item2)
            End Operator
            <Method(inline)>
            Public Shared Widening Operator CType(Input As fraction) As Decimal
                Return CDec(Input.top) / CDec(Input.low)
            End Operator

            Public Overrides Function ToString() As String
                Return String.Format("{0}/{1}", top, low)
            End Function
        End Structure

    End Namespace

End Namespace
