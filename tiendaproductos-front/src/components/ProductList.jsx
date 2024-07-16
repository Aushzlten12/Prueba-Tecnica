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
