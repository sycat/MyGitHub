Imports Oracle.DataAccess.Types
Imports Oracle.DataAccess.Client
Imports System.Xml
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.IO

''' <summary>
''' This process is only for test.
''' </summary>
''' <remarks></remarks>
Module Module1

    Sub Main()

        Try
            '測試 LINQ
            'Dim contactDoc As XDocument = _
            '    <?xml version="1.0"?>
            '    <contact>
            '        <name>Patrick Hines</name>
            '        <phone type="home">206-555-0144</phone>
            '        <phone type="work">425-555-0145</phone>
            '    </contact>

            'Dim xd As XDocument = XDocument.Load("..\..\Test.xml")
            'Dim employees As IEnumerable(Of XElement) =
            '    From el In xd.Root.<Items>.<Item>
            '    Where el.@PartNumber = "926-AA"
            '    Select el

            'For Each employee As XElement In employees

            '    Console.WriteLine(employee.<ProductName>.Value)
            'Next

            'Dim s As String = "開頭1.<BR />2.<BR />3."
            ''s = Regex.Replace(s, "(?<!<BR />)\d+\.", New MatchEvaluator(AddressOf AddHtmlNewLines), RegexOptions.Compiled Or RegexOptions.IgnoreCase)
            's = Regex.Replace(s, "<br />", "\n", RegexOptions.IgnoreCase)
            'Console.WriteLine(s)

            TestLINQ()

            'MaskByRegex()

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Console.ReadKey()

    End Sub

    Private Function AddHtmlNewLines(ByVal match As Match) As String
        Return "<BR />" + match.Value
    End Function

    Public Function StripTags(ByVal src As String, ByVal reservedTagPool As String()) As String
        Return System.Text.RegularExpressions.Regex.Replace(
            src,
            String.Format("<(?!{0}).*?>", String.Join("|", reservedTagPool)),
            String.Empty)
    End Function

    Public Function IsHtmlEndorseVer(ByVal os As String, ByVal ver As String) As Boolean
        Dim result As Boolean = False
        Dim iosSupportVer As String = "1.2.7"

        If os.Equals("IN") And ver >= iosSupportVer Then
            result = True
        End If

        Return result
    End Function

    Public Sub MaskByRegex()
        Dim pattern As String = "<CCardNo>.*</CCardNo>"

        Dim name As String = "<?xml version=""1.0"" encoding=""UTF-8"" ?><QueryBooking><SYSCode>IN</SYSCode><LanguageCode>zh_tw</LanguageCode><OSVersion>7.1</OSVersion><AppVersion>1.2.8</AppVersion><DeviceID>3G</DeviceID><MachineName>iPhone4</MachineName><ContentVersion>5.7</ContentVersion><QueryData><Pax_PID>Z123456780</Pax_PID><Pax_PPT/><PNR>CEK346</PNR><CCardNo>1111111111111100</CCardNo></QueryData></QueryBooking>"


        Console.WriteLine(Regex.Replace(name,
                                        pattern,
                                        New MatchEvaluator(Function(match As Match)
                                                               If match.Value.Length < 35 Then
                                                                   ' 沒有輸入卡號 或 不正確的卡號長度
                                                                   Return match.Value
                                                               Else
                                                                   Return match.Value.Substring(0, 13) & "********" & match.Value.Substring(21)
                                                               End If
                                                           End Function),
                                        RegexOptions.Compiled Or RegexOptions.IgnoreCase))

    End Sub

    Public Function CompareVersion(ByVal source As String, ByVal target As String) As Integer
        Dim result As Integer = 0
        Dim sourceAry As Integer()
        Dim targetAry As Integer()
        Dim tempList As New List(Of Integer)

        tempList.Clear()
        For Each s As String In source.Split(".")
            tempList.Add(Convert.ToInt32(s))
        Next
        sourceAry = tempList.ToArray()

        tempList.Clear()
        For Each s As String In target.Split(".")
            tempList.Add(Convert.ToInt32(s))
        Next
        targetAry = tempList.ToArray()

        For i As Integer = 0 To targetAry.Length - 1
            If sourceAry(i) > targetAry(i) Then
                result = 1
                Exit For
            End If

            If sourceAry(i) < targetAry(i) Then
                result = -1
                Exit For
            End If
        Next

        Return result
    End Function

    Public Sub TestLINQ()
        Dim r As String = String.Empty

        Dim xd As XDocument =
            <?xml version="1.0" encoding="UTF-8"?>
            <QueryBookingList>
                <SYSCode>IN</SYSCode>
                <LanguageCode>zh_tw</LanguageCode>
                <OSVersion>7.1</OSVersion>
                <AppVersion>1.2.8</AppVersion>
                <DeviceID>3G</DeviceID>
                <MachineName>iPhone4</MachineName>
                <ContentVersion>5.7</ContentVersion>
                <QueryData>
                    <PNR></PNR>
                    <CCardNo>1111111111111111</CCardNo>
                    <Pax_PID></Pax_PID>
                    <Pax_PPT>123</Pax_PPT>
                </QueryData>
            </QueryBookingList>

        'Dim count As Integer = (From param In xd.<QueryBookingList>.<QueryData>
        '                 Where param.<CCardNo>.Value <> String.Empty And
        '                    (param.<Pax_PID>.Value <> String.Empty Or param.<Pax_PPT>.Value <> String.Empty)
        '                 Select param).Count()
        Dim count As Integer = (From param In xd.<QueryBookingList>.<QueryData>
                                Where param.<CCardNo>.Value <> String.Empty
                                Select param).Count()

        'Console.WriteLine(xd.Element("QueryBookingList").Element("QueryData").Element("Pax_PPT").Value)


        Dim xd2 As XDocument =
            <?xml version="1.0" encoding="UTF-8"?>
            <ItinTag value="Y">
                <FlyQual>
                </FlyQual>
            </ItinTag>


        Dim xe2 As XElement =
            <QueryData>
                <PNR></PNR>
                <CCardNo>1111111111111111</CCardNo>
            </QueryData>

        Dim i As Integer = 1

        xd2.Element("ItinTag").Add(<PNRInfo value=<%= i %>>
                                       <PNR>CEK346</PNR>
                                       <FltDate>2014/08/06</FltDate>
                                       <DepCty>TSA</DepCty>
                                       <ArrCty>TTT</ArrCty>
                                   </PNRInfo>)
        Dim wr As New StringWriter()
        xd2.Save(wr)
        'Console.WriteLine(xd2.Declaration.ToString() & xd2.ToString(SaveOptions.DisableFormatting))
        'Console.WriteLine(xe2.<CCardNo>.Value)
        'Console.WriteLine(wr.GetStringBuilder().ToString())

        'xe2.Add(<ETktNbr>123</ETktNbr>)
        'Console.WriteLine(xe2.ToString())

        Dim tktList As XElement =
            <ETImageAry>
                <ETktQual>
                    <ETktPaxENname>LI/PENYI</ETktPaxENname>
                    <ETktNbr>5252434802491</ETktNbr>
                    <EtktETktPaxCNname>李本壹</EtktETktPaxCNname>
                    <ETktPaxPID>H116741805</ETktPaxPID>
                    <ETktSeqAry ETktBook="" ETktTtlBook="">
                        <ETktSeqQual>
                            <EtktSegNbr>1</EtktSegNbr>
                            <ETktArnkIndi></ETktArnkIndi>
                            <ETktCarrCd>B7</ETktCarrCd>
                            <ETktFltNbr>851</ETktFltNbr>
                            <ETktBkgCd>Y</ETktBkgCd>
                            <ETktFltDate>13AUG14</ETktFltDate>
                            <ETktDpeApt>TSA</ETktDpeApt>
                            <ETktArrApt>TTT</ETktArrApt>
                            <ETktDpeTime>0700</ETktDpeTime>
                            <ETktSegStat>OK</ETktSegStat>
                            <ETktFareBss>YU</ETktFareBss>
                            <ETktUsgSTAT></ETktUsgSTAT>
                        </ETktSeqQual>
                        <ETktSeqQual>
                            <EtktSegNbr>2</EtktSegNbr>
                            <ETktArnkIndi></ETktArnkIndi>
                            <ETktCarrCd>B7</ETktCarrCd>
                            <ETktFltNbr>856</ETktFltNbr>
                            <ETktBkgCd>Y</ETktBkgCd>
                            <ETktFltDate>16AUG14</ETktFltDate>
                            <ETktDpeApt>TTT</ETktDpeApt>
                            <ETktArrApt>TSA</ETktArrApt>
                            <ETktDpeTime>1445</ETktDpeTime>
                            <ETktSegStat>OK</ETktSegStat>
                            <ETktFareBss>YU</ETktFareBss>
                            <ETktUsgSTAT></ETktUsgSTAT>
                        </ETktSeqQual>
                    </ETktSeqAry>
                    <ETktRLoc>CAUBW4</ETktRLoc>
                </ETktQual>
                <ETktQual>
                    <ETktPaxENname>LI/PENYI</ETktPaxENname>
                    <ETktNbr>5252434802493</ETktNbr>
                    <EtktETktPaxCNname>李本壹</EtktETktPaxCNname>
                    <ETktPaxPID>H116741805</ETktPaxPID>
                    <ETktSeqAry ETktBook="" ETktTtlBook="">
                        <ETktSeqQual>
                            <EtktSegNbr>1</EtktSegNbr>
                            <ETktArnkIndi></ETktArnkIndi>
                            <ETktCarrCd>B7</ETktCarrCd>
                            <ETktFltNbr>339</ETktFltNbr>
                            <ETktBkgCd>Y</ETktBkgCd>
                            <ETktFltDate>12AUG14</ETktFltDate>
                            <ETktDpeApt>TSA</ETktDpeApt>
                            <ETktArrApt>MFK</ETktArrApt>
                            <ETktDpeTime>1735</ETktDpeTime>
                            <ETktSegStat>OK</ETktSegStat>
                            <ETktFareBss>YU</ETktFareBss>
                            <ETktUsgSTAT></ETktUsgSTAT>
                        </ETktSeqQual>
                    </ETktSeqAry>
                    <ETktRLoc>CCX8UU</ETktRLoc>
                </ETktQual>
                <ETktQual>
                    <ETktPaxENname>LI/PENYI</ETktPaxENname>
                    <ETktNbr>5252434802470</ETktNbr>
                    <EtktETktPaxCNname>李本壹</EtktETktPaxCNname>
                    <ETktPaxPID>H116741805</ETktPaxPID>
                    <ETktSeqAry ETktBook="" ETktTtlBook="">
                        <ETktSeqQual>
                            <EtktSegNbr>1</EtktSegNbr>
                            <ETktArnkIndi></ETktArnkIndi>
                            <ETktCarrCd>B7</ETktCarrCd>
                            <ETktFltNbr>852</ETktFltNbr>
                            <ETktBkgCd>Y</ETktBkgCd>
                            <ETktFltDate>12AUG14</ETktFltDate>
                            <ETktDpeApt>TTT</ETktDpeApt>
                            <ETktArrApt>TSA</ETktArrApt>
                            <ETktDpeTime>0830</ETktDpeTime>
                            <ETktSegStat>OK</ETktSegStat>
                            <ETktFareBss>YU</ETktFareBss>
                            <ETktUsgSTAT></ETktUsgSTAT>
                        </ETktSeqQual>
                        <ETktSeqQual>
                            <EtktSegNbr>2</EtktSegNbr>
                            <ETktArnkIndi></ETktArnkIndi>
                            <ETktCarrCd>B7</ETktCarrCd>
                            <ETktFltNbr>857</ETktFltNbr>
                            <ETktBkgCd>Y</ETktBkgCd>
                            <ETktFltDate>15AUG14</ETktFltDate>
                            <ETktDpeApt>TSA</ETktDpeApt>
                            <ETktArrApt>TTT</ETktArrApt>
                            <ETktDpeTime>1615</ETktDpeTime>
                            <ETktSegStat>OK</ETktSegStat>
                            <ETktFareBss>YU</ETktFareBss>
                            <ETktUsgSTAT></ETktUsgSTAT>
                        </ETktSeqQual>
                    </ETktSeqAry>
                    <ETktRLoc>CGUQ7T</ETktRLoc>
                </ETktQual>
                <ETktQual>
                    <ETktPaxENname>WANG/CHIUYING</ETktPaxENname>
                    <ETktNbr>5252434763076</ETktNbr>
                    <EtktETktPaxCNname>王秋英</EtktETktPaxCNname>
                    <ETktPaxPID>D220167904</ETktPaxPID>
                    <ETktSeqAry ETktBook="" ETktTtlBook="">
                        <ETktSeqQual>
                            <EtktSegNbr>1</EtktSegNbr>
                            <ETktArnkIndi></ETktArnkIndi>
                            <ETktCarrCd>B7</ETktCarrCd>
                            <ETktFltNbr>OPEN</ETktFltNbr>
                            <ETktBkgCd>Y</ETktBkgCd>
                            <ETktFltDate></ETktFltDate>
                            <ETktDpeApt>TNN</ETktDpeApt>
                            <ETktArrApt>KNH</ETktArrApt>
                            <ETktDpeTime></ETktDpeTime>
                            <ETktSegStat></ETktSegStat>
                            <ETktFareBss>Y/GH00</ETktFareBss>
                            <ETktUsgSTAT>USED</ETktUsgSTAT>
                        </ETktSeqQual>
                    </ETktSeqAry>
                    <ETktRLoc>CHH2SX</ETktRLoc>
                </ETktQual>
                <ETktQual>
                    <ETktPaxENname>LI/CHIHCHINYU</ETktPaxENname>
                    <ETktNbr>5252434763108</ETktNbr>
                    <EtktETktPaxCNname>黎氏金尤</EtktETktPaxCNname>
                    <ETktPaxPID>E260000167</ETktPaxPID>
                    <ETktSeqAry ETktBook="" ETktTtlBook="">
                        <ETktSeqQual>
                            <EtktSegNbr>1</EtktSegNbr>
                            <ETktArnkIndi></ETktArnkIndi>
                            <ETktCarrCd>B7</ETktCarrCd>
                            <ETktFltNbr>652</ETktFltNbr>
                            <ETktBkgCd>Y</ETktBkgCd>
                            <ETktFltDate>05AUG14</ETktFltDate>
                            <ETktDpeApt>MZG</ETktDpeApt>
                            <ETktArrApt>TNN</ETktArrApt>
                            <ETktDpeTime>1930</ETktDpeTime>
                            <ETktSegStat>OK</ETktSegStat>
                            <ETktFareBss>EU</ETktFareBss>
                            <ETktUsgSTAT>USED</ETktUsgSTAT>
                        </ETktSeqQual>
                    </ETktSeqAry>
                    <ETktRLoc>CBRX3W</ETktRLoc>
                </ETktQual>
            </ETImageAry>

        'Dim xe4 =
        '    <Result>
        '        <%= From tkt In tktList.<ETImageAry>.<ETktQual>.<ETktSeqAry>.<ETktSeqQual>
        '          Select <type><%= DateProcess.RevertSpecialFormat(tkt.<ETktFltDate>.Value).ToString("yyyy/MM/dd") %></type>
        '        %>
        '    </Result>



        'Dim etList = From tkt In tktList.<ETImageAry>.<ETktQual>
        '             Select tkt

        Dim matchListXE As XElement =
            <QueryBooking_R>
                <ErrCd/>
                <ErrOrgMsg/>
                <Message>Msg</Message>
                <ItinModifyMessage/>
                <MatchList>
                    <ValidList count="2">
                        <PNRInfo>
                            <PNR>CGUQ7T</PNR>
                            <FltDate>2014/08/15</FltDate>
                            <DepCty>TSA</DepCty>
                            <ArrCty>TTT</ArrCty>
                        </PNRInfo>
                        <PNRInfo>
                            <PNR>CAUBW4</PNR>
                            <FltDate>2014/08/16</FltDate>
                            <DepCty>TTT</DepCty>
                            <ArrCty>TSA</ArrCty>
                        </PNRInfo>
                    </ValidList>
                    <OverList count="1">
                        <PNRInfo>
                            <PNR>CCX8UU</PNR>
                            <FltDate>2014/08/12</FltDate>
                            <DepCty>TSA</DepCty>
                            <ArrCty>MFK</ArrCty>
                        </PNRInfo>
                    </OverList>
                </MatchList>
            </QueryBooking_R>


        Dim re As String = If((matchListXE.Elements("Message").Any()), matchListXE.Element("Message").Value.ToUpper(), String.Empty)

        Console.WriteLine(re)


    End Sub

End Module