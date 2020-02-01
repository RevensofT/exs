Imports loi = System.Collections.Generic.List(Of System.Int32)
Imports loii = System.Collections.Generic.List(Of System.Int32())

Namespace EXS.Math
    Public Module Exten
        Public ReadOnly pi As fraction = (245850922, 78256779)

        <Extension, Method(inline)>
        Public Sub fib(Max As uint, Process As Action(Of uint))
            Call (x:=0UL, y:=1UL).do((Process, Max),
                                     Function(V, S) V.x > S.Max,
                                     Function(V, S)
                                         S.Process(V.x)
                                         Return (V.y, V.x + V.y)
                                     End Function)
        End Sub

        <Extension, Method(inline)>
        Public Function min(Of T)(Input As T(), Start_index As Int32, Is_lesser As Func(Of T, T, Boolean)) As Int32
            If Start_index = Input.Length - 1 Then Return Start_index
            Return Input.Length.run(Start_index + 1, 1, (Input, Less:=Is_lesser), Start_index,
                                    Function(I, D, O) If(D.Less(D.Input(I), D.Input(O)), I, O))
        End Function

        '''<summary>Selection sort</summary>
        <Extension, Method(inline)>
        Public Function sort(Of T)(Input As T(), Is_lesser As Func(Of T, T, Boolean)) As T()
            Input.update(Is_lesser, Sub(G, S, I) G.swap(I, Input.min(I, S)))
            Return Input
        End Function

        <Extension, Method(inline)>
        Public Function subset(Base As Int32) As Int32()()
            Return (Sets:=Base.run(New loii(Base), Sub(I, O) O.Add({I})), Base, Tmp:=New loi(Base), X:=Base, Y:=0, L:=Base, N:=0).
            do(Function(X) X.X < 1, Function(X) (X.
            do(Function(Y) Y.L <= Y.Y, Function(Y) (Y.
            do(Function(Z) Z.Sets(Z.Y).find(Z.N, Function(R, L) R = L) >= 0,
               Function(Z)
                   Z.Tmp.AddRange(Z.Sets(Z.Y))
                   Z.Tmp.Add(Z.N)
                   If Z.Y = 0 OrElse
                       Z.Tmp.Count <> Z.Sets(Z.Y - 1).Length Then
                       Z.Sets.Add(Z.Tmp.ToArray)
                   End If
                   Z.Tmp.Clear()
                   Z.N += 1
                   Return Z '(Z.Sets, Z.Base, Z.X, Z.Y, Z.L, Z.N + 1)
               End Function).
            Sets, Y.Base, Y.Tmp, Y.X, Y.Y + 1, Y.L, 0)).
            Sets, X.Base, X.Tmp, X.X - 1, X.L, X.Sets.Count, 0)).
            Sets.ToArray
        End Function

        <Extension, Method(inline)>
        Public Function sums(Data() As Int32) As Int64
            Return Data.process(0L, Function(V, I) V + I)
        End Function

        <Extension, Method(inline)>
        Public Function subset_sum(Sample() As Int32, Target As Int64) As Int32()()
            Return subset(Sample.Length).
                   process(New loii, (Sample, Target),
                           Function(O, D, I)
                               If I.update(D, Sub(S, Di, Ii) S(Ii) = D.Sample(S(Ii))).
                                    sums = D.Target Then O.Add(I)
                               Return O
                           End Function).ToArray
        End Function
    End Module

End Namespace