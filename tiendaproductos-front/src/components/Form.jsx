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
