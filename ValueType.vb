Namespace EXS

    'Module Generator
    '    Public Sub gen_vtc()
    '        Dim Froto = Function(SB As Text.StringBuilder, N As Integer, Body As Func(Of Integer, String))
    '                        Dim I As Int32
    '                        For I = 1 To N
    '                            SB.Append($"{Body(I)}, ")
    '                        Next
    '                        Return SB.Append(Body(I))
    '                    End Function

    '        With New Text.StringBuilder
    '            For i = 2 To 8
    '                Dim T0 = Froto(.Clear, i, Function(N) $"T{N}").ToString
    '                Dim T1 = Froto(.Clear, i, Function(N) $"Field{N} As T{N}").ToString
    '                Dim T2 = Froto(.Clear, i, Function(N) $"Field{N}").ToString
    '                Dim T7 = Froto(.Clear, i, Function(N) $"field_{N} As T{N}").ToString

    '                Diagnostics.Debug.WriteLine(.Clear.
    '                            AppendLine("<Method(inline)>").
    '                            AppendLine($"Public Shared Function [class](Of {T0})({T1}) As (token As int, {T7})").
    '                            AppendLine($"Return (Info.token(Of T).value, {T2})").
    '                            AppendLine("End Function").
    '                    ToString)
    '            Next
    '        End With
    '    End Sub

    '    Public Sub gen_vta()
    '        Dim Froto = Function(SB As Text.StringBuilder, N As Integer, Body As Func(Of Integer, String))
    '                        Dim I As Int32
    '                        For I = 0 To N - 1
    '                            SB.Append($"{Body(I)}, ")
    '                        Next
    '                        Return SB.Append(Body(I))
    '                    End Function

    '        With New Text.StringBuilder
    '            For i = 1 To 15
    '                Dim T0 = Froto(.Clear, i, Function(N) $"T{N}").ToString
    '                Dim T1 = Froto(.Clear, i, Function(N) $"Index_{N} As T").ToString
    '                Dim T2 = Froto(.Clear, i, Function(N) $"Index_{N}").ToString
    '                Dim T7 = Froto(.Clear, i, Function(N) $"index_{N} As T").ToString

    '                Diagnostics.Debug.WriteLine(.Clear.
    '                            AppendLine("<Method(inline)>").
    '                            AppendLine($"Public Shared Function alloc({T1}) As (token As int, length As int, {T7})").
    '                            AppendLine($"Return (Info.token(Of T()).value, {i + 1}, {T2})").
    '                            AppendLine("End Function").
    '                    ToString)
    '            Next
    '        End With
    '    End Sub
    'End Module

    Public Structure vclass(Of T As Class)
        <Method(inline)>
        Public Shared Function alloc(Of T1)(Field1 As T1) As (token As int, field_1 As T1)
            Return (Info.token(Of T).value, Field1)
        End Function
        <Method(inline)>
        Public Shared Function alloc(Of T1, T2)(Field1 As T1, Field2 As T2) As (token As int, field_1 As T1, field_2 As T2)
            Return (Info.token(Of T).value, Field1, Field2)
        End Function
        <Method(inline)>
        Public Shared Function alloc(Of T1, T2, T3)(Field1 As T1, Field2 As T2, Field3 As T3) As (token As int, field_1 As T1, field_2 As T2, field_3 As T3)
            Return (Info.token(Of T).value, Field1, Field2, Field3)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Of T1, T2, T3, T4)(Field1 As T1, Field2 As T2, Field3 As T3, Field4 As T4) As (token As int, field_1 As T1, field_2 As T2, field_3 As T3, field_4 As T4)
            Return (Info.token(Of T).value, Field1, Field2, Field3, Field4)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Of T1, T2, T3, T4, T5)(Field1 As T1, Field2 As T2, Field3 As T3, Field4 As T4, Field5 As T5) As (token As int, field_1 As T1, field_2 As T2, field_3 As T3, field_4 As T4, field_5 As T5)
            Return (Info.token(Of T).value, Field1, Field2, Field3, Field4, Field5)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Of T1, T2, T3, T4, T5, T6)(Field1 As T1, Field2 As T2, Field3 As T3, Field4 As T4, Field5 As T5, Field6 As T6) As (token As int, field_1 As T1, field_2 As T2, field_3 As T3, field_4 As T4, field_5 As T5, field_6 As T6)
            Return (Info.token(Of T).value, Field1, Field2, Field3, Field4, Field5, Field6)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Of T1, T2, T3, T4, T5, T6, T7)(Field1 As T1, Field2 As T2, Field3 As T3, Field4 As T4, Field5 As T5, Field6 As T6, Field7 As T7) As (token As int, field_1 As T1, field_2 As T2, field_3 As T3, field_4 As T4, field_5 As T5, field_6 As T6, field_7 As T7)
            Return (Info.token(Of T).value, Field1, Field2, Field3, Field4, Field5, Field6, Field7)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Of T1, T2, T3, T4, T5, T6, T7, T8)(Field1 As T1, Field2 As T2, Field3 As T3, Field4 As T4, Field5 As T5, Field6 As T6, Field7 As T7, Field8 As T8) As (token As int, field_1 As T1, field_2 As T2, field_3 As T3, field_4 As T4, field_5 As T5, field_6 As T6, field_7 As T7, field_8 As T8)
            Return (Info.token(Of T).value, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8)
        End Function
    End Structure

    Public Structure varray(Of T)
        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T) As (token As int, length As int, index_0 As T, index_1 As T)
            Return (Info.token(Of T()).value, 2, Index_0, Index_1)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T)
            Return (Info.token(Of T()).value, 3, Index_0, Index_1, Index_2)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T)
            Return (Info.token(Of T()).value, 4, Index_0, Index_1, Index_2, Index_3)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T, Index_4 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T, index_4 As T)
            Return (Info.token(Of T()).value, 5, Index_0, Index_1, Index_2, Index_3, Index_4)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T, Index_4 As T, Index_5 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T, index_4 As T, index_5 As T)
            Return (Info.token(Of T()).value, 6, Index_0, Index_1, Index_2, Index_3, Index_4, Index_5)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T, Index_4 As T, Index_5 As T, Index_6 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T, index_4 As T, index_5 As T, index_6 As T)
            Return (Info.token(Of T()).value, 7, Index_0, Index_1, Index_2, Index_3, Index_4, Index_5, Index_6)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T, Index_4 As T, Index_5 As T, Index_6 As T, Index_7 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T, index_4 As T, index_5 As T, index_6 As T, index_7 As T)
            Return (Info.token(Of T()).value, 8, Index_0, Index_1, Index_2, Index_3, Index_4, Index_5, Index_6, Index_7)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T, Index_4 As T, Index_5 As T, Index_6 As T, Index_7 As T, Index_8 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T, index_4 As T, index_5 As T, index_6 As T, index_7 As T, index_8 As T)
            Return (Info.token(Of T()).value, 9, Index_0, Index_1, Index_2, Index_3, Index_4, Index_5, Index_6, Index_7, Index_8)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T, Index_4 As T, Index_5 As T, Index_6 As T, Index_7 As T, Index_8 As T, Index_9 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T, index_4 As T, index_5 As T, index_6 As T, index_7 As T, index_8 As T, index_9 As T)
            Return (Info.token(Of T()).value, 10, Index_0, Index_1, Index_2, Index_3, Index_4, Index_5, Index_6, Index_7, Index_8, Index_9)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T, Index_4 As T, Index_5 As T, Index_6 As T, Index_7 As T, Index_8 As T, Index_9 As T, Index_10 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T, index_4 As T, index_5 As T, index_6 As T, index_7 As T, index_8 As T, index_9 As T, index_10 As T)
            Return (Info.token(Of T()).value, 11, Index_0, Index_1, Index_2, Index_3, Index_4, Index_5, Index_6, Index_7, Index_8, Index_9, Index_10)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T, Index_4 As T, Index_5 As T, Index_6 As T, Index_7 As T, Index_8 As T, Index_9 As T, Index_10 As T, Index_11 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T, index_4 As T, index_5 As T, index_6 As T, index_7 As T, index_8 As T, index_9 As T, index_10 As T, index_11 As T)
            Return (Info.token(Of T()).value, 12, Index_0, Index_1, Index_2, Index_3, Index_4, Index_5, Index_6, Index_7, Index_8, Index_9, Index_10, Index_11)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T, Index_4 As T, Index_5 As T, Index_6 As T, Index_7 As T, Index_8 As T, Index_9 As T, Index_10 As T, Index_11 As T, Index_12 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T, index_4 As T, index_5 As T, index_6 As T, index_7 As T, index_8 As T, index_9 As T, index_10 As T, index_11 As T, index_12 As T)
            Return (Info.token(Of T()).value, 13, Index_0, Index_1, Index_2, Index_3, Index_4, Index_5, Index_6, Index_7, Index_8, Index_9, Index_10, Index_11, Index_12)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T, Index_4 As T, Index_5 As T, Index_6 As T, Index_7 As T, Index_8 As T, Index_9 As T, Index_10 As T, Index_11 As T, Index_12 As T, Index_13 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T, index_4 As T, index_5 As T, index_6 As T, index_7 As T, index_8 As T, index_9 As T, index_10 As T, index_11 As T, index_12 As T, index_13 As T)
            Return (Info.token(Of T()).value, 14, Index_0, Index_1, Index_2, Index_3, Index_4, Index_5, Index_6, Index_7, Index_8, Index_9, Index_10, Index_11, Index_12, Index_13)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T, Index_4 As T, Index_5 As T, Index_6 As T, Index_7 As T, Index_8 As T, Index_9 As T, Index_10 As T, Index_11 As T, Index_12 As T, Index_13 As T, Index_14 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T, index_4 As T, index_5 As T, index_6 As T, index_7 As T, index_8 As T, index_9 As T, index_10 As T, index_11 As T, index_12 As T, index_13 As T, index_14 As T)
            Return (Info.token(Of T()).value, 15, Index_0, Index_1, Index_2, Index_3, Index_4, Index_5, Index_6, Index_7, Index_8, Index_9, Index_10, Index_11, Index_12, Index_13, Index_14)
        End Function

        <Method(inline)>
        Public Shared Function alloc(Index_0 As T, Index_1 As T, Index_2 As T, Index_3 As T, Index_4 As T, Index_5 As T, Index_6 As T, Index_7 As T, Index_8 As T, Index_9 As T, Index_10 As T, Index_11 As T, Index_12 As T, Index_13 As T, Index_14 As T, Index_15 As T) As (token As int, length As int, index_0 As T, index_1 As T, index_2 As T, index_3 As T, index_4 As T, index_5 As T, index_6 As T, index_7 As T, index_8 As T, index_9 As T, index_10 As T, index_11 As T, index_12 As T, index_13 As T, index_14 As T, index_15 As T)
            Return (Info.token(Of T()).value, 16, Index_0, Index_1, Index_2, Index_3, Index_4, Index_5, Index_6, Index_7, Index_8, Index_9, Index_10, Index_11, Index_12, Index_13, Index_14, Index_15)
        End Function
    End Structure

End Namespace