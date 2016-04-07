Imports System.Data.sqlclient
Imports System.Configuration
Imports System
Imports System.Data
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Public Class md5Fun
    Public gtextROR As String
    Public Shared connectPrj As New SqlConnection(ConfigurationManager.ConnectionStrings("connectionString").ConnectionString)

    ''************************************************************************************************************************************************
    ''STARTS PF common functions
    ''************************************************************************************************************************************************
    Public Shared Function md5Encr(ByVal strPassword As String) As String
        'Encrypt the password 
        Dim md5Hasher As New MD5CryptoServiceProvider()
        Dim hashedBytes As Byte()
        Dim encoder As New UTF8Encoding()
        hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(strPassword.Trim))

        ''''Create a new Stringbuilder to collect the bytes and create a string.
        Dim sBuilder As New StringBuilder
        ' ''''Loop through each byte of the hashed data  and format each one as a hexadecimal string.
        Dim i As Integer
        For i = 0 To hashedBytes.Length - 1
            sBuilder.Append(hashedBytes(i).ToString("x2"))
        Next i
        md5Encr = sBuilder.ToString
    End Function

    Public Shared Function dbCurrentDate(ByVal connectPrj As SqlConnection) As String
        If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
        Dim cmd As New SqlCommand("select to_char(sysdate,'dd-MON-yy HH:MI:SS') from dual", connectPrj)
        dbCurrentDate = cmd.ExecuteScalar
        connectPrj.Close()
    End Function

    Public Shared Function checkExist(ByVal connectPrj As SqlConnection, ByVal txtkey As String) As String
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim cmd As New SqlCommand("select count(*) from    prj_user_table_new where userid= @userid", connectPrj)
            cmd.Parameters.Add(New SqlParameter("@userid", txtkey))
            If cmd.ExecuteScalar > 0 Then
                checkExist = "EXISTS"
            Else
                checkExist = "NO"
            End If
            connectPrj.Close()
        Catch ex As Exception
            checkExist = "Page error!"
        End Try
    End Function

    Public Shared Function dt_effect_data() As String
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim cmd As New SqlCommand("select dt_upload from tbl_upload_dt", connectPrj)
            dt_effect_data = IIf(IsDBNull(cmd.ExecuteNonQuery), "", cmd.ExecuteNonQuery)
            connectPrj.Close()
        Catch ex As Exception
            dt_effect_data = ""
        End Try
    End Function

    Public Shared Function ValidYear(ByVal txtFinYear As String, ByRef mstring As String) As Boolean
        ValidYear = False
        Dim temp As Integer
        temp = Val(Mid(txtFinYear.Trim, 1, 4)) + 1

        If Mid(txtFinYear.Trim, 6, 2) <> Mid(Trim(Str(temp)), 3, 2) Or _
        Not IsNumeric(Val(Mid(txtFinYear.Trim, 1, 4))) Or Not IsNumeric(Val(Mid(txtFinYear.Trim, 6, 2))) Or _
        txtFinYear.Trim.Length <> 7 Or Trim(Mid(txtFinYear.Trim, 5, 1)) <> "-" Then
            mstring = "☺ Invalid Fanancial Year Format! ♂ (YYYY-YY)"
        Else
            mstring = ""
            ValidYear = True
        End If

    End Function
    Public Shared Function getNextYear(ByVal txtFinYear As String) As String
        getNextYear = Val(Mid(Trim(txtFinYear), 1, 4)) + 1 & "-" & Mid(Val(Mid(Trim(txtFinYear), 1, 4)) + 2, 3, 2)
    End Function
    Public Shared Function getPrevYear(ByVal txtFinYear As String) As String
        getPrevYear = Val(Mid(Trim(txtFinYear), 1, 4)) - 1 & "-" & Mid(Trim(txtFinYear), 3, 2)
    End Function
    Public Shared Function StrtoDate(ByVal dateString As String) As Date
        Try
            'Dim supportedFormats() As String = New String() {"M/d/yy", "M/d/yyyy", "MM/dd/yy", "MM/dd/yyyy", "dd/MM/yy", "dd-MMM-yyyy", "dd-MM-yy", "dd-MM-yyyy", "dd-MMM-yyyy", "dd-MMM-yyyy", "ddMMMyy", "ddMMMyyyy"}
            'StrtoDate = DateTime.ParseExact(dateString, supportedFormats, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None)

            StrtoDate = DateTime.ParseExact(dateString, "dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ").DateTimeFormat) '.CurrentCulture, System.Globalization.DateTimeStyles.None)
        Catch ex As Exception
            Console.Write("Error")
        End Try
    End Function
    Public Shared Function dtDOR(ByVal dtdob As Date) As Date
        On Error GoTo date_error
        Dim strDate As String
        Dim strMonth As String
        strDate = DatePart("d", dtdob)
        Select Case strDate
            Case "1"
                dtDOR = DateAdd("yyyy", 60, dtdob)
                dtDOR = DateAdd("d", -1, dtDOR)
            Case Else
                dtDOR = DateAdd("yyyy", 60, dtdob)
                strMonth = Month(dtDOR)
                Select Case strMonth
                    Case "1", "3", "5", "7", "8", "10", "12"
                        dtDOR = DateAdd("yyyy", 60, dtdob)
                        dtDOR = DateAdd("d", 31 - strDate, dtDOR)
                    Case "4", "6", "9", "11"
                        dtDOR = DateAdd("yyyy", 60, dtdob)
                        dtDOR = DateAdd("d", 30 - strDate, dtDOR)
                    Case "2"
                        Dim strYear As String
                        Dim strTemp As String
                        dtDOR = DateAdd("yyyy", 60, dtdob)
                        strYear = Year(dtDOR)
                        strTemp = Mid(strYear, Len(strYear) - 1)

                        Select Case strTemp
                            Case "00"
                                If (strYear Mod 400) = 0 Then
                                    dtDOR = DateAdd("d", 29 - strDate, dtDOR)
                                Else
                                    dtDOR = DateAdd("d", 28 - strDate, dtDOR)
                                End If
                            Case Else
                                If (strYear Mod 4) = 0 Then
                                    dtDOR = DateAdd("d", 29 - strDate, dtDOR)
                                Else
                                    dtDOR = DateAdd("d", 28 - strDate, dtDOR)
                                End If
                        End Select
                End Select
        End Select
        Exit Function
date_error:
        'MsgBox(Err.Description)
    End Function

    Public Shared Function monNumToName(ByVal mon As Integer) As String
        Select Case mon
            Case Is = 1
                monNumToName = "Jan"
            Case Is = 2
                monNumToName = "Feb"
            Case Is = 3
                monNumToName = "Mar"
            Case Is = 4
                monNumToName = "Apr"
            Case Is = 5
                monNumToName = "May"
            Case Is = 6
                monNumToName = "Jun"
            Case Is = 7
                monNumToName = "Jul"
            Case Is = 8
                monNumToName = "Aug"
            Case Is = 9
                monNumToName = "Sep"
            Case Is = 10
                monNumToName = "Oct"
            Case Is = 11
                monNumToName = "Nov"
            Case Is = 12
                monNumToName = "Dec"
            Case Else
                monNumToName = "Invalid Month"
        End Select
    End Function

    Public Shared Function getdecimal(ByVal v_num As Double) As Double
        Dim p_decimal As Integer
        Dim v_len, d_amt As Integer
        Dim v_amt, v_amt1 As String
        v_amt = Trim(Str(v_num))
        v_len = Len(v_amt)
        p_decimal = InStr(v_amt, ".")
        v_amt1 = Mid(v_amt, p_decimal + 1, 1)
        d_amt = Val(v_amt1)
        If p_decimal > 0 Then
            If d_amt >= 5 Then
                getdecimal = Val(Mid(v_amt, 1, p_decimal - 1)) + 1

            Else
                getdecimal = Val(Mid(v_amt, 1, p_decimal - 1))
            End If
        Else
            getdecimal = Val(v_amt)
        End If
    End Function
    Public Shared Function d_pt(ByVal v_num As Double) As String
        d_pt = Str(v_num) & ".00"
    End Function

    Public Shared Function lpad(ByVal v_str As String, ByVal num As Integer) As String
        Dim a, b, c As Integer
        Dim S As String = ""
        c = 1
        a = Len(v_str)
        b = num - a
        For c = 1 To b
            S = S & " "
        Next
        v_str = S & v_str
        lpad = v_str
    End Function

    Public Shared Function rpad(ByVal v_str As String, ByVal num As Integer) As String
        Dim a, b, c As Integer
        Dim S As String = ""
        c = 1
        a = Len(v_str)
        b = num - a
        For c = 1 To b
            S = S & " "
        Next
        v_str = v_str & S
        rpad = v_str
    End Function

    ''check for completed fin year
    Public Shared Function fyrCompleted(ByVal fyr As String) As Boolean
        fyrCompleted = False
        If (Now.Year = (Mid(fyr, 1, 4) + 1) And Now.Month >= 4) Or Now.Year > (Mid(fyr, 1, 4) + 1) Then fyrCompleted = True
    End Function


End Class
