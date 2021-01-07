using Mun_HwaJae.classes;
using System.Xml; //  공공데이터 api를 사용하기 위해 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;

using System.Net.Configuration;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Controls;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Xml.XPath;
using System.Diagnostics.Eventing.Reader;

namespace Mun_HwaJae
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            resultBox.Text = String.Empty;
            addreTxt.Text = String.Empty;
          }
        private void Click_Click(object sender, RoutedEventArgs e)
        {
            //넘겨 받은 단어 따라 시도코드와 바인딩
            string addre = addreTxt.Text; //addreTxt에 있던 단어를 넘겨 받음 
            string[] result = addre.Split(new string[] { " " }, StringSplitOptions.None); // 단어들을 띄어쓰기로 구분해서 배열에 넣음 
            string ccbaCtcd = "ZZ";
            if (result[0] == "서울" || result[0] == "서울 특별시")
            {
                ccbaCtcd = "11";
            }
            else if (result[0] == "부산")
            {
                ccbaCtcd = "21";
            }
            else if (result[0] == "대구")
            {
                ccbaCtcd = "22";
            }
            else if (result[0] == "인천")
            {
                ccbaCtcd = "23";
            }
            else if (result[0] == "광주")
            {
                ccbaCtcd = "24";
            }
            else if (result[0] == "대전")
            {
                ccbaCtcd = "25";
            }
            else if (result[0] == "울산")
            {
                ccbaCtcd = "26";
            }
            else if (result[0] == "세종")
            {
                ccbaCtcd = "45";
            }
            else if (result[0] == "경기" || result[0] == "경기도")
            {
                ccbaCtcd = "31";
            }
            else if (result[0] == "강원" || result[0] == "강원도")
            {
                ccbaCtcd = "32";
            }
            else if (result[0] == "충북" || result[0] == "충청북도")
            {
                ccbaCtcd = "33";
            }
            else if (result[0] == "충남" || result[0] == "충청남도")
            {
                ccbaCtcd = "34";
            }
            else if (result[0] == "전북" || result[0] == "전라북도")
            {
                ccbaCtcd = "35";
            }
            else if (result[0] == "전남" || result[0] == "전라남도")
            {
                ccbaCtcd = "36";
            }
            else if (result[0] == "경북" || result[0] == "경상북도")
            {
                ccbaCtcd = "37";
            }else if(result[0] == "경남" || result[0] == "경상남도")
            {
                ccbaCtcd = "38";
            }else if (result[0] == "제주" || result[0] == "제주도")
            {
                ccbaCtcd = "50";
            }else
            {
                ccbaCtcd = "ZZ";
            }

            
            string url = " http://www.cha.go.kr/cha/SearchKindOpenapiList.do?ccbaCtcd=" + ccbaCtcd;
            

            try //문화재청 api정보를 불러오는 코드 
            {
                //Xml 정보 Load 
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(url);

                //노드의 정렬된 컬렉션( /result/item으로 된 노드들만 가져옴
                XmlNodeList itemNodes = XmlDoc.SelectNodes("/result/item");
                /*컬렉션에서 문서 단일 노드 추출
                 (/result/item 노드 밑에서 longtitude(경도), latitude(위도), ccbaCtcdNm(시도명), ccsiName(시군구명), ccbaMnm1(문화재명) 단일 노드 추출)*/
                foreach (XmlNode itemNode in itemNodes)
                {
                    XmlNode longtitudeNode = itemNode.SelectSingleNode("longitude"); //경도 
                    XmlNode latitudeNode = itemNode.SelectSingleNode("latitude"); //위도
                    XmlNode ccbaCtcdNmNode = itemNode.SelectSingleNode("ccbaCtcdNm"); //시도명
                    XmlNode ccsiNameNode = itemNode.SelectSingleNode("ccsiName"); //시군구명
                    XmlNode ccbaMnm1Node = itemNode.SelectSingleNode("ccbaMnm1"); //문화재명(국문)
                    //시군구명에 맞게 한 번 더 필터링 해주는 조건문
                    String cs = ccsiNameNode.InnerText;
                    if (result[1].Contains(cs))
                    {
                        resultBox.Text += "문화재 명(국문) : \n" + ccbaMnm1Node.InnerText + "\n\n";
                        if (longtitudeNode.InnerText == "0" && latitudeNode.InnerText == "0")
                        {
                            resultBox.Text = " 입력하신 주소지에는 위도, 경도가 없습니다.\n  상세주소를 참고하시길 바랍니다.\n";

                        }
                    }
                }

            }

            catch (Exception ex)
            { 
                Console.WriteLine(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }



        }
    }
}

