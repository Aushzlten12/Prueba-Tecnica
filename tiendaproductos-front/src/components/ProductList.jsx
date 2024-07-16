import React from "react";
import { List, Button, Skeleton } from "antd";

const ProductList = ({ products, loading }) => {
  if (loading) {
    return (
      <div style={{ textAlign: "center", marginTop: 12 }}>
        <Skeleton active />
      </div>
    );
  }

  return (
    <div>
      <List
        className="demo-loadmore-list"
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
              <Button type="primary" key="view">
                Editar
              </Button>,
              <Button type="default" key="add">
                Agregar
              </Button>,
            ]}
          >
            <List.Item.Meta
              title={item.nombre}
              description={`${item.precio} soles`}
            />
          </List.Item>
        )}
      />
    </div>
  );
};

export default ProductList;
