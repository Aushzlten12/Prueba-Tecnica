# Tienda de Productos - Frontend

Se ha usado React como se pidio en los requerimientos de la prueba además de Ant Design una librería de componentes.

## Hook
Se necesito de un Hook que en este caso es **useFetch** para realizar acciones en el backend antes creado con .NET

```js
import { useState, useEffect, useCallback } from "react";

const useFetch = (url) => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [postLoading, setPostLoading] = useState(false);
  const [postError, setPostError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      setError(null);
      try {
        const response = await fetch(url);
        console.log(response);
        if (!response.ok) throw new Error();
        const result = await response.json();
        console.log(result);
        setData(result);
      } catch (err) {
        setError(err);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, [url]);

  const postData = useCallback(
    async (postData) => {
      setPostLoading(true);
      setPostError(null);
      // console.log(postData);
      let DtoProducto = {
        name: postData.nombre,
        price: postData.precio,
      };
      // console.log(DtoProducto);
      try {
        const response = await fetch(url, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(DtoProducto),
        });
        if (!response.ok) throw new Error();
        const result = await response.json();
        // console.log(result);
        return result;
      } catch (err) {
        setPostError(err);
        console.log(err);
      } finally {
        setPostLoading(false);
      }
    },
    [url]
  );

  return {
    data,
    loading,
    error,
    postLoading,
    postError,
    postData,
  };
};

export default useFetch;
```

Ya que se pedía mostrar una lista de productos y que se pueda agregar productos solo usamos el método GET y POST de la API.

## Home
El contenido principal es el siguiente:
```js
import React, { useState, useEffect } from "react";
import useFetch from "../hooks/useFetch";
import ProductList from "../components/ProductList";
import { Typography } from "antd";
import ProductForm from "../components/Form";

const { Title } = Typography;

const Home = () => {
  const [products, setProducts] = useState([]);
  const { loading, error, data } = useFetch("http://localhost:5098/productos");

  useEffect(() => {
    if (data) {
      setProducts(data);
    }
  }, [data]);
  return (
    <div>
      <Title style={{ textAlign: "center" }}>Lista de Productos</Title>
      <ProductForm
        onProductAdded={(newProduct) =>
          setProducts((prevProducts) => [...prevProducts, newProduct])
        }
      />
      <ProductList products={products} loading={loading} error={error} />
    </div>
  );
};

export default Home;

```

Este cuenta con dos componentes los cuales son: el formulario como *ProductForm* y la lista de productos como *ProductList*.

# Form
```js 
import React from "react";
import { Form, Input, InputNumber, Button, message } from "antd";
import useFetch from "./../hooks/useFetch";

const ProductForm = ({ onProductAdded }) => {
  const { postData, postLoading } = useFetch("http://localhost:5098/productos");
  const [form] = Form.useForm();

  const onFinish = async (values) => {
    try {
      const result = await postData(values);
      if (result) {
        message.success("Producto agregado exitosamente!");
        form.resetFields();
        onProductAdded(result);
      }
    } catch {
      message.error("Error al agregar el producto: ");
    }
  };

  return (
    <Form
      form={form}
      name="productForm"
      onFinish={onFinish}
      layout="vertical"
      initialValues={{
        nombre: "",
        precio: 1,
      }}
    >
      <Form.Item
        name="nombre"
        label="Nombre del Producto"
        rules={[
          {
            required: true,
            message: "Por favor ingrese el nombre del producto",
          },
          {
            type: "string",
            max: 50,
            message:
              "El nombre del producto no puede tener más de 50 caracteres",
          },
        ]}
      >
        <Input placeholder="Nombre del Producto" />
      </Form.Item>

      <Form.Item
        name="precio"
        label="Precio"
        rules={[
          {
            required: true,
            message: "Por favor ingrese el precio del producto",
          },
          {
            type: "number",
            min: 1,
            max: 50000,
            message: "El precio debe estar entre 1 y 50000",
          },
        ]}
      >
        <InputNumber
          min={1}
          placeholder="Precio"
          style={{ width: "100%" }}
          step={0.01}
        />
      </Form.Item>

      <Form.Item>
        <Button type="primary" htmlType="submit" loading={postLoading}>
          Agregar Producto
        </Button>
      </Form.Item>
    </Form>
  );
};

export default ProductForm;

```
Haciendo uso de la librería Ant, para poder crear un formulario según los requerimientos y contando con validaciones necesarias.

# List

```js 
import React from "react";
import { List, Button, Skeleton, Typography } from "antd";

const { Text } = Typography;

const ProductList = ({ products, loading, error }) => {
  if (loading) {
    return (
      <div style={{ textAlign: "center", marginTop: 12 }}>
        <Skeleton active />
      </div>
    );
  }

  if (error) {
    return (
      <div style={{ textAlign: "center", marginTop: 12 }}>
        <Text type="danger">
          Error al cargar los productos: {error.message}
        </Text>
      </div>
    );
  }

  return (
    <div>
      <List
        itemLayout="horizontal"
        bordered
        dataSource={products}
        renderItem={(item) => (
          <List.Item
            style={{
              display: "flex",
              justifyContent: "space-between",
              alignItems: "center",
            }}
            actions={[
              <Button type="dashed" key="view">
                Producto
              </Button>,
              <Button
                type="default"
                key="delete"
                style={{ fontWeight: "bold" }}
              >
                {item.id}
              </Button>,
            ]}
          >
            <List.Item.Meta
              title={item.nombre}
              description={`${item.precio.toFixed(2)} soles`}
            />
          </List.Item>
        )}
      />
    </div>
  );
};

export default ProductList;

```
Mostrando los productos haciendo uso de **useFetch** y de la librería Ant-Design, se colocó en cada Item, su nombre, precio y su id.

La idea en el futuro sería agregar botones para editar y poder eliminar ciertos productos, pero eso no se ha requerido para esta prueba.

# Images

Se muestra el funcionamiento de la aplicación

![inicio](/tiendaproductos-front//images/inicio.PNG)

Se tiene un formulario para agregar productos en la parte superior y debajo se muestra la lista de productos.

![validation-name](/tiendaproductos-front/images/validationName.PNG)

Se muestra la validación para el nombre del producto, con la cantidad maxima de 50 caracteres.

![validation-price](/tiendaproductos-front/images/validationPricce.PNG)

La validación también para el precio, que en este caso debe estar en un rango entre 1 y 50000 es decir no negativos tampoco.

![new-product](/tiendaproductos-front/images/newProduct.PNG)

El resultado de agregar un producto