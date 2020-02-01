Namespace EXS
    Namespace Pre
#Region "scan"
        Public Structure scan(Of T, V, S)
            Public Delegate Function flink(Extra As V, Addon As S, [Do] As Func(Of V, S, T, V), ByRef Last_element As T, ByRef First_element As T) As V
            Friend Shared ReadOnly ing As flink =
                "la.3.4 <:0 la.2.0.1.4 lot.0 used.0 la.1.2.3.4 mt.0 + re use-me ; :0 la.0".
                compile(Of flink).used(Of Func(Of V, S, T, V)).type(Of T).fin
        End Structure

        Public Structure scan(Of T, V)
            'Public Delegate Sub item(Extra As V, Element As T)
            Public Delegate Sub link([Do] As Action(Of V, T), Extra As V, ByRef Last_element As T, ByRef First_element As T)
            Friend Shared ReadOnly ner As link =
                "la.2.3 <:0 la.0.1.3 lot.0 used.0 la.0.1.2.3 mt.0 + re use-me :0".
                compile(Of link).used(Of Action(Of V, T)).type(Of T).fin

            Public Delegate Function flink(Extra As V, [Do] As Func(Of V, T, V), ByRef Last_element As T, ByRef First_element As T) As V
            Friend Shared ReadOnly ing As flink =
                "la.2.3 <:0 la.1.0.3 lot.0 used.0 la.1.2.3 mt.0 + re use-me ; :0 la.0".
                compile(Of flink).used(Of Func(Of V, T, V)).type(Of T).fin
        End Structure
        Public Structure ref_scan(Of T, V)

            Public Delegate Sub item(ByRef Extra As V, ByRef Element As T)
            Public Delegate Sub link([Do] As item, ByRef Extra As V, ByRef Last_element As T, ByRef First_element As T)
            Friend Shared ReadOnly ner As link =
                "la.2.3 <:0 la.0.1.3 used.0 la.0.1.2.3 mt.0 + re use-me :0".
                compile(Of link).used(Of item).type(Of T).fin

        End Structure
        Public Structure scan(Of T)

            'Public Delegate Sub item(Element As T)
            Public Delegate Sub link([Do] As Action(Of T), ByRef Last_element As T, ByRef First_element As T)
            Friend Shared ReadOnly ner As link =
                "la.1.2 <:0 la.0.2 lot.0 used.0 la.0.1.2 mt.0 + re use-me :0".
                compile(Of link).used(Of Action(Of T)).type(Of T).fin
        End Structure
        Public Structure ref_scan(Of T)

            Public Delegate Sub item(ByRef Element As T)
            Public Delegate Sub link([Do] As item, ByRef Last_element As T, ByRef First_element As T)
            Friend Shared ReadOnly ner As link =
                "la.1.2 <:0 la.0.2 used.0 la.0.1.2 mt.0 + re use-me :0".
                compile(Of link).used(Of item).type(Of T).fin

        End Structure
#End Region


#Region "recur"
        Public Structure recur(Of T, V)
            Public Delegate Function rundown(Data() As T, Start As Int32, Extra As V, Is_end As Func(Of T, V, Boolean)) As Int32
            Friend Shared ReadOnly sion As rundown =
                "la.1 -- sa.1 la.1 .0 <:0 la.3.0.1 let.0 la.2 used.0 t:0 jmp-me :0 la.1".
                compile(Of rundown).
                type(Of T).
                used(Of Func(Of T, V, Boolean)).
                fin

            Public Delegate Function load(Data As T, Extra As V, [Until] As Func(Of T, V, Boolean), Recur As Func(Of T, V, T)) As T
            Friend Shared ReadOnly sive As load =
                "la.2.0.1 used.0 f:0 la.0 ; :0 la.3.0.1 used.1 la.1.2.3 re use-me".
                compile(Of load).
                used(Of Func(Of T, V, Boolean), Func(Of T, V, T)).
                fin

            Public Delegate Function link(I As Int32, Data As T, Output As V) As V
            Public Delegate Function def(Start As Int32, [Stop] As Int32, Change As Int32,
                                         Data As T, Initialize_output As V, [Do] As link) As V
            Friend Shared ReadOnly ing As def =
                "la.5.0.3.4 used.0 sa.4 la.0.2 + sa.0 la.0.1 =:0 jmp-me :0 la.4".
                compile(Of def).used(Of link).fin
        End Structure
        Public Structure recur(Of T)
            Public Delegate Function rundown(Data() As T, Start As Int32, Is_end As Func(Of T, Boolean)) As Int32
            Friend Shared ReadOnly sion As rundown =
                "la.1 -- sa.1 la.1 .0 <:0 la.2.0.1 let.0 used.0 t:0 jmp-me :0 la.1".
                compile(Of rundown).
                type(Of T).
                used(Of Func(Of T, Boolean)).
                fin

            Public Delegate Function load(Data As T, [Until] As Func(Of T, Boolean), Recur As Func(Of T, T)) As T
            Friend Shared ReadOnly sive As load =
                "la.1.0 used.0 f:0 la.0 ; :0 la.2.0 used.1 la.1.2 re use-me".
                compile(Of load).
                used(Of Func(Of T, Boolean), Func(Of T, T)).
                fin

            Public Delegate Sub link(I As Int32, Data As T)
            Public Delegate Sub def(Start As Int32, [Stop] As Int32, Change As Int32, Data As T, [Do] As link)
            Friend Shared ReadOnly ing As def =
                "la.4.0.3 used.0 la.0.2 + sa.0 la.0.1 =:0 jmp-me :0".
                compile(Of def).used(Of link).fin
        End Structure
        Public Structure recur
            Public Delegate Sub link(I As Int32)
            Public Delegate Sub def(Start As Int32, [Stop] As Int32, Change As Int32, [Do] As link)
            Friend Shared ReadOnly ing As def =
                "la.3.0 used.0 la.0.2 + sa.0 la.0.1 =:0 jmp-me :0".
                compile(Of def).used(Of link).fin
        End Structure
#End Region

    End Namespace

    Partial Public Module SubEx
        <Extension, Method(inline)>
        Public Function process(Of T, V, S)(Data() As T, Inintialize_output As V, Extra_info As S, Processing As Func(Of V, S, T, V)) As V
            Return Pre.scan(Of T, V, S).ing(Inintialize_output, Extra_info, Processing, Data(Data.Length - 1), Data(0))
        End Function
        <Extension, Method(inline)>
        Public Function process(Of T, V)(Data() As T, Inintialize_output As V, Processing As Func(Of V, T, V)) As V
            Return Pre.scan(Of T, V).ing(Inintialize_output, Processing, Data(Data.Length - 1), Data(0))
        End Function

        <Extension, Method(inline)>
        Public Function [each](Of T, V)(Data() As T, Extra_info As V, Process As Action(Of V, T)) As T()
            Pre.scan(Of T, V).ner(Process, Extra_info, Data(Data.Length - 1), Data(0))
            Return Data
        End Function
        <Extension, Method(inline)>
        Public Function [each](Of T)(Data() As T, Process As Action(Of T)) As T()
            Pre.scan(Of T).ner(Process, Data(Data.Length - 1), Data(0))
            Return Data
        End Function

        <Extension, Method(inline)>
        Public Function update(Of T, V)(Data() As T, ByRef Update_info As V, Process As Pre.ref_scan(Of T, V).item) As T()
            Pre.ref_scan(Of T, V).ner(Process, Update_info, Data(Data.Length - 1), Data(0))
            Return Data
        End Function
        <Extension, Method(inline)>
        Public Function update(Of T, V)(Data() As T, Update_info As V, Process As Action(Of T(), V, Int32)) As T()
            Return Data.Length.run((Data, Update_info, Process), Sub(I, G) G.Process(G.Data, G.Update_info, I)).Data
        End Function

        <Extension, Method(inline)>
        Public Function run(Of T)([Stop] As Int32, Data As T, Process As Pre.recur(Of T, T).link) As T
            Return Pre.recur(Of T, T).ing(0, [Stop], 1, Data, Nothing, Process)
        End Function
        <Extension, Method(inline)>
        Public Function run(Of T)([Stop] As Int32, Data As T, [Do] As Pre.recur(Of T).link) As T
            Pre.recur(Of T).ing(0, [Stop], 1, Data, [Do])
            Return Data
        End Function

    End Module

    Partial Public Module Exten
        <Extension, Method(inline)>
        Public Function process(Of T, V, S)(ByRef Last As T, ByRef First As T, Input As V, Extra As S, Processing As Func(Of V, S, T, V)) As V
            Return Pre.scan(Of T, V, S).ing(Input, Extra, Processing, Last, First)
        End Function
        <Extension, Method(inline)>
        Public Function process(Of T, V)(ByRef Last As T, ByRef First As T, Input As V, Processing As Func(Of V, T, V)) As V
            Return Pre.scan(Of T, V).ing(Input, Processing, Last, First)
        End Function

        <Extension, Method(inline)>
        Public Sub scan(Of T, V)(ByRef Last As T, ByRef First As T, ByRef Extra As V, [Do] As Pre.ref_scan(Of T, V).item)
            Pre.ref_scan(Of T, V).ner([Do], Extra, Last, First)
        End Sub
        <Extension, Method(inline)>
        Public Sub scan(Of T, V)(ByRef Last As T, ByRef First As T, Extra As V, [Do] As Action(Of V, T))
            Pre.scan(Of T, V).ner([Do], Extra, Last, First)
        End Sub
        <Extension, Method(inline)>
        Public Sub scan(Of T)(ByRef Last As T, ByRef First As T, [Do] As Pre.ref_scan(Of T).item)
            Pre.ref_scan(Of T).ner([Do], Last, First)
        End Sub
        <Extension, Method(inline)>
        Public Sub scan(Of T)(ByRef Last As T, ByRef First As T, [Do] As Action(Of T))
            Pre.scan(Of T).ner([Do], Last, First)
        End Sub

        <Extension, Method(inline)>
        Public Function [do](Of V, S)(Variant_data As V, Static_data As S,
                                      Until As Func(Of V, S, Boolean),
                                      [Loop] As Func(Of V, S, V)) As V
            Return Pre.recur(Of V, S).sive(Variant_data, Static_data, Until, [Loop])
        End Function
        <Extension, Method(inline)>
        Public Function [do](Of T)(Data As T, Until As Func(Of T, Boolean), [Loop] As Func(Of T, T)) As T
            Return Pre.recur(Of T).sive(Data, Until, [Loop])
        End Function

        <Extension, Method(inline)>
        Public Function find(Of T, V)(Data() As T, Extra_data As V, Check As Func(Of T, V, Boolean)) As Int32
            Return Pre.recur(Of T, V).sion(Data, Data.Length, Extra_data, Check)
        End Function
        <Extension, Method(inline)>
        Public Function find(Of T)(Data() As T, Check As Func(Of T, Boolean)) As Int32
            Return Pre.recur(Of T).sion(Data, Data.Length, Check)
        End Function

        <Extension, Method(inline)>
        Public Function run(Of T, V)([Stop] As Int32, Start As Int32, Change As Int32,
                                     Data As T, Initialize_output As V, [Do] As Pre.recur(Of T, V).link) As V
            Return Pre.recur(Of T, V).ing(Start, [Stop], Change, Data, Initialize_output, [Do])
        End Function
        <Extension, Method(inline)>
        Public Sub run(Of T)([Stop] As Int32, Start As Int32, Change As Int32, Data As T, [Do] As Pre.recur(Of T).link)
            Pre.recur(Of T).ing(Start, [Stop], Change, Data, [Do])
        End Sub
        <Extension, Method(inline)>
        Public Sub run([Stop] As Int32, Start As Int32, Change As Int32, [Do] As Pre.recur.link)
            Pre.recur.ing(Start, [Stop], Change, [Do])
        End Sub
    End Module
End Namespace