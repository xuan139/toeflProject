using System;
using System.Collections;
using System.Xml;

namespace UnileverCAS.UnileverFun
{
 public class XmlHelper
 {
 #region ��������
     XmlDocument xmldoc;
     XmlNode xmlnode;
     XmlElement xmlelem;
 #endregion

 #region ����Xml�ĵ�
/// <summary>
 /// ����һ�����и��ڵ��Xml�ļ�
/// </summary>
 /// <param name="FileName">Xml�ļ�����</param>
 /// <param name="rootName">���ڵ�����</param>
 /// <param name="Encode">���뷽ʽ:gb2312��UTF-8�ȳ�����</param>
 /// <param name="DirPath">�����Ŀ¼·��</param>
 /// <returns></returns>
 public bool CreateXmlDocument(string FileName, string RootName, string Encode)
 {
     try
     {
         xmldoc = new XmlDocument();
         XmlDeclaration xmldecl;
         xmldecl = xmldoc.CreateXmlDeclaration("1.0", Encode,null);
         xmldoc.AppendChild(xmldecl);
         xmlelem = xmldoc.CreateElement("", RootName, "");
         xmldoc.AppendChild(xmlelem);
         xmldoc.Save(FileName);
         return true;
     }
     catch (Exception e)
     {
         return false;
         throw new Exception(e.Message);
     }
 }

 #endregion

 #region ���ò�������(��ɾ��)
 /// <summary>
 /// ����һ���ڵ�����������ӽڵ�
/// </summary>
 /// <param name="XmlFile">Xml�ļ�·��</param>
 /// <param name="NewNodeName">����Ľڵ�����</param>
 /// <param name="HasAttributes">�˽ڵ��Ƿ�������ԣ�TrueΪ�У�FalseΪ��</param>
 /// <param name="fatherNode">�˲���ڵ�ĸ��ڵ�,Ҫƥ���XPath���ʽ(����:"//�ڵ���//�ӽڵ���)</param>
 /// <param name="htAtt">�˽ڵ�����ԣ�KeyΪ��������ValueΪ����ֵ</param>
 /// <param name="htSubNode">�ӽڵ�����ԣ�KeyΪName,ValueΪInnerText</param>
 /// <returns>������Ϊ���³ɹ�������ʧ��</returns>
 public bool InsertNode(string XmlFile, string NewNodeName, bool HasAttributes, string fatherNode, Hashtable htAtt, Hashtable htSubNode)
 {
     try
     {
             xmldoc = new XmlDocument();
             xmldoc.Load(XmlFile);
             XmlNode root = xmldoc.SelectSingleNode(fatherNode);
             xmlelem = xmldoc.CreateElement(NewNodeName);
         if (htAtt != null && HasAttributes)//���˽ڵ������ԣ������������
        {
             SetAttributes(xmlelem, htAtt);
             SetNodes(xmlelem.Name, xmldoc, xmlelem, htSubNode);//�����˽ڵ����Ժ�����������ӽڵ�����ǵ�InnerText
         }
         else
         {
            SetNodes(xmlelem.Name, xmldoc, xmlelem, htSubNode);//���˽ڵ������ԣ���ôֱ����������ӽڵ�
        }

             root.AppendChild(xmlelem);
             xmldoc.Save(XmlFile);

         return true;
     }
     catch (Exception e)
     {

     throw new Exception(e.Message);

     }
 }
 /// <summary>
 /// ���½ڵ�
/// </summary>
 /// <param name="XmlFile">Xml�ļ�·��</param>
 /// <param name="fatherNode">��Ҫ���½ڵ���ϼ��ڵ�,Ҫƥ���XPath���ʽ(����:"//�ڵ���//�ӽڵ���)</param>
 /// <param name="htAtt">��Ҫ���µ����Ա�Key������Ҫ���µ����ԣ�Value������º��ֵ</param>
 /// <param name="htSubNode">��Ҫ���µ��ӽڵ�����Ա�Key������Ҫ���µ��ӽڵ�����Name,Value������º��ֵInnerText</param>
 /// <returns>������Ϊ���³ɹ�������ʧ��</returns>
 public bool UpdateNode(string XmlFile, string fatherNode, Hashtable htAtt, Hashtable htSubNode)
 {
     try
     {
         xmldoc = new XmlDocument();
         xmldoc.Load(XmlFile);
         XmlNodeList root = xmldoc.SelectSingleNode(fatherNode).ChildNodes;
         UpdateNodes(root, htAtt, htSubNode);
         xmldoc.Save(XmlFile);
         return true;
     }
     catch (Exception e)
     {
        throw new Exception(e.Message);
     }
 }

 /// <summary>
 /// ɾ��ָ���ڵ��µ��ӽڵ�
/// </summary>
 /// <param name="XmlFile">Xml�ļ�·��</param>
 /// <param name="fatherNode">�ƶ��ڵ�,Ҫƥ���XPath���ʽ(����:"//�ڵ���//�ӽڵ���)</param>
 /// <returns>������Ϊ���³ɹ�������ʧ��</returns>
 public bool DeleteNodes(string XmlFile, string fatherNode)
 {
     try
     {
         xmldoc = new XmlDocument();
         xmldoc.Load(XmlFile);
         xmlnode = xmldoc.SelectSingleNode(fatherNode);
         xmlnode.RemoveAll();
         xmldoc.Save(XmlFile);
         return true;
     }
     catch (XmlException xe)
     {
         throw new XmlException(xe.Message);
     }
 }
 /*��������*/
 /// <summary>
 /// ɾ��ƥ��XPath���ʽ�ĵ�һ���ڵ�(�ڵ��е���Ԫ��ͬʱ�ᱻɾ��)
 /// </summary>
 /// <param name="xmlFileName">XML�ĵ���ȫ�ļ���(��������·��)</param>
 /// <param name="xpath">Ҫƥ���XPath���ʽ(����:"//�ڵ���//�ӽڵ���</param>
 /// <returns>�ɹ�����true,ʧ�ܷ���false</returns>
 public bool DeleteXmlNodeByXPath(string xmlFileName, string xpath)
 {
     bool isSuccess = false;
     xmldoc = new XmlDocument();
     try
     {
            xmldoc.Load(xmlFileName); //����XML�ĵ�
            XmlNode xmlNode = xmldoc.SelectSingleNode(xpath);
         if (xmlNode != null)
         {
         //ɾ���ڵ�
             xmldoc.ParentNode.RemoveChild(xmlNode);
         }
             xmldoc.Save(xmlFileName); //���浽XML�ĵ�
            isSuccess = true;
         }
     catch (Exception ex)
     {
     throw ex; //������Զ������Լ����쳣����
    }
    return isSuccess;
 }

 /* keleyi.com */
 /// <summary>
 /// ɾ��ƥ��XPath���ʽ�ĵ�һ���ڵ��е�ƥ�����xmlAttributeName������
/// </summary>
 /// <param name="xmlFileName">XML�ĵ���ȫ�ļ���(��������·��)</param>
 /// <param name="xpath">Ҫƥ���XPath���ʽ(����:"//�ڵ���//�ӽڵ���</param>
 /// <param name="xmlAttributeName">Ҫɾ����xmlAttributeName����������</param>
 /// <returns>�ɹ�����true,ʧ�ܷ���false</returns>
 public bool DeleteXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName)
 {
 bool isSuccess = false;
 bool isExistsAttribute = false;
 xmldoc = new XmlDocument();
 try
 {
 xmldoc.Load(xmlFileName); //����XML�ĵ�
XmlNode xmlNode = xmldoc.SelectSingleNode(xpath);
 XmlAttribute xmlAttribute = null;
 if (xmlNode != null)
 {
 //����xpath�ڵ��е���������
foreach (XmlAttribute attribute in xmlNode.Attributes)
 {
 if (attribute.Name.ToLower() == xmlAttributeName.ToLower())
 {
 //�ڵ��д��ڴ�����
xmlAttribute = attribute;
 isExistsAttribute = true;
 break;
 }
 }
 if (isExistsAttribute)
 {
 //ɾ���ڵ��е�����
xmlNode.Attributes.Remove(xmlAttribute);
 }
 }
 xmldoc.Save(xmlFileName); //���浽XML�ĵ�
isSuccess = true;
 }
 catch (Exception ex)
 {
 throw ex; //������Զ������Լ����쳣����
}
return isSuccess;
 }

 /*������*/
 /// <summary>
 /// ɾ��ƥ��XPath���ʽ�ĵ�һ���ڵ��е���������
/// </summary>
 /// <param name="xmlFileName">XML�ĵ���ȫ�ļ���(��������·��)</param>
 /// <param name="xpath">Ҫƥ���XPath���ʽ(����:"//�ڵ���//�ӽڵ���</param>
 /// <returns>�ɹ�����true,ʧ�ܷ���false</returns>
 public bool DeleteAllXmlAttributeByXPath(string xmlFileName, string xpath)
 {
 bool isSuccess = false;
 xmldoc = new XmlDocument();
 try
 {
 xmldoc.Load(xmlFileName); //����XML�ĵ�
XmlNode xmlNode = xmldoc.SelectSingleNode(xpath);
 if (xmlNode != null)
 {
 //����xpath�ڵ��е���������
xmlNode.Attributes.RemoveAll();
 }
 xmldoc.Save(xmlFileName); //���浽XML�ĵ�
isSuccess = true;
 }
 catch (Exception ex)
 {
 throw ex; //������Զ������Լ����쳣����
}
return isSuccess;
 }
 #endregion

 #region ˽�з���
/// <summary>
 /// ���ýڵ�����
/// </summary>
 /// <param name="xe">�ڵ�������Element</param>
 /// <param name="htAttribute">�ڵ����ԣ�Key�����������ƣ�Value��������ֵ</param>
 private void SetAttributes(XmlElement xe, Hashtable htAttribute)
 {
 foreach (DictionaryEntry de in htAttribute)
 {
 xe.SetAttribute(de.Key.ToString(), de.Value.ToString());
 }
 }
 /// <summary>
 /// �����ӽڵ㵽���ڵ���
/// </summary>
 /// <param name="rootNode">�ϼ��ڵ�����</param>
 /// <param name="XmlDoc">Xml�ĵ�</param>
 /// <param name="rootXe">�����ڵ�������Element</param>
 /// <param name="SubNodes">�ӽڵ����ԣ�KeyΪNameֵ��ValueΪInnerTextֵ</param>
 private void SetNodes(string rootNode, XmlDocument XmlDoc, XmlElement rootXe, Hashtable SubNodes)
 {
 if (SubNodes == null)
 return;
 foreach (DictionaryEntry de in SubNodes)
 {
 xmlnode = XmlDoc.SelectSingleNode(rootNode);
 XmlElement subNode = XmlDoc.CreateElement(de.Key.ToString());
 subNode.InnerText = de.Value.ToString();
 rootXe.AppendChild(subNode);
 }
 }
 /// <summary>
 /// ���½ڵ����Ժ��ӽڵ�InnerTextֵ���� �� ��
/// </summary>
 /// <param name="root">���ڵ�����</param>
 /// <param name="htAtt">��Ҫ���ĵ��������ƺ�ֵ</param>
 /// <param name="htSubNode">��Ҫ����InnerText���ӽڵ����ֺ�ֵ</param>
 private void UpdateNodes(XmlNodeList root, Hashtable htAtt, Hashtable htSubNode)
 {
 foreach (XmlNode xn in root)
 {
 xmlelem = (XmlElement)xn;
 if (xmlelem.HasAttributes)//����ڵ������ԣ����ȸ�����������
{
foreach (DictionaryEntry de in htAtt)//�������Թ�ϣ��
{
if (xmlelem.HasAttribute(de.Key.ToString()))//����ڵ�����Ҫ���ĵ�����
{
xmlelem.SetAttribute(de.Key.ToString(), de.Value.ToString());//��ѹ�ϣ������Ӧ��ֵValue����������Key
 }
 }
 }
 if (xmlelem.HasChildNodes)//������ӽڵ㣬���޸����ӽڵ��InnerText
 {
 XmlNodeList xnl = xmlelem.ChildNodes;
 foreach (XmlNode xn1 in xnl)
 {
 XmlElement xe = (XmlElement)xn1;
 foreach (DictionaryEntry de in htSubNode)
 {
 if (xe.Name == de.Key.ToString())//htSubNode�е�key�洢����Ҫ���ĵĽڵ����ƣ�
{
xe.InnerText = de.Value.ToString();//htSubNode�е�Value�洢��Key�ڵ���º������
}
 }
 }
 }

 }
 }
 #endregion
 #region XML�ĵ��ڵ��ѯ�Ͷ�ȡ
/**/
 /// <summary>
 /// ѡ��ƥ��XPath���ʽ�ĵ�һ���ڵ�XmlNode.
 /// </summary>
 /// <param name="xmlFileName">XML�ĵ���ȫ�ļ���(��������·��)</param>
 /// <param name="xpath">Ҫƥ���XPath���ʽ(����:"//�ڵ���//�ӽڵ���")</param>
 /// <returns>����XmlNode</returns>
 public XmlNode GetXmlNodeByXpath(string xmlFileName, string xpath)
 {
 xmldoc = new XmlDocument();
 try
 {
 xmldoc.Load(xmlFileName); //����XML�ĵ�
XmlNode xmlNode = xmldoc.SelectSingleNode(xpath);
 return xmlNode;
 }
 catch (Exception ex)
 {
 return null;
 //throw ex; //������Զ������Լ����쳣����
}
 }

 /**/
 /// <summary>
 /// ѡ��ƥ��XPath���ʽ�Ľڵ��б�XmlNodeList.
 /// </summary>
 /// <param name="xmlFileName">XML�ĵ���ȫ�ļ���(��������·��)</param>
 /// <param name="xpath">Ҫƥ���XPath���ʽ(����:"//�ڵ���//�ӽڵ���")</param>
 /// <returns>����XmlNodeList</returns>
 public XmlNodeList GetXmlNodeListByXpath(string xmlFileName, string xpath)
 {
 xmldoc = new XmlDocument();
 try
 {
 xmldoc.Load(xmlFileName); //����XML�ĵ�
XmlNodeList xmlNodeList = xmldoc.SelectNodes(xpath);
 return xmlNodeList;
 }
 catch (Exception ex)
 {
 return null;
 //throw ex; //������Զ������Լ����쳣����
}
 }

 /**/
 /// <summary>
 /// ѡ��ƥ��XPath���ʽ�ĵ�һ���ڵ��ƥ��xmlAttributeName������XmlAttribute. ������
/// </summary>
 /// <param name="xmlFileName">XML�ĵ���ȫ�ļ���(��������·��)</param>
 /// <param name="xpath">Ҫƥ���XPath���ʽ(����:"//�ڵ���//�ӽڵ���</param>
 /// <param name="xmlAttributeName">Ҫƥ��xmlAttributeName����������</param>
 /// <returns>����xmlAttributeName</returns>
 public XmlAttribute GetXmlAttribute(string xmlFileName, string xpath, string xmlAttributeName)
 {
 string content = string.Empty;
 xmldoc = new XmlDocument();
 XmlAttribute xmlAttribute = null;
 try
 {
 xmldoc.Load(xmlFileName); //����XML�ĵ�
XmlNode xmlNode = xmldoc.SelectSingleNode(xpath);
 if (xmlNode != null)
 {
 if (xmlNode.Attributes.Count > 0)
 {
 xmlAttribute = xmlNode.Attributes[xmlAttributeName];
 }
 }
 }
 catch (Exception ex)
 {
 throw ex; //������Զ������Լ����쳣����
}
return xmlAttribute;
 }
 #endregion
 }
}



//�������ôʹ���أ��������һ������xml�ĵ������ӣ�
//XmlHelper m_menu_keleyi_com = new XmlHelper();
//m_menu_keleyi_com.CreateXmlDocument(@"D:\kel"+"eyimenu.xml", "ke"+"leyimenu", "utf-8");

//��̴�����D�̴�����һ����Ϊkeleyimenu.xml���ĵ����ĵ��и��ڵ�keleyimenu,�ĵ�������Ϊ��
//<?xml version="1.0" encoding="utf-8"?>
//<keleyimenu />

//ʹ��XmlHelper�����ڵ㣺http://keleyi.com/a/bjac/0avt4d6l.htm

//����ת���Կ�����http://keleyi.com/a/bjac/ttssua0f.htm