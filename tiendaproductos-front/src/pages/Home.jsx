import React from "react";
import useFetch from "../hooks/useFetch";
import ProductList from "../components/ProductList";
import { Typography } from "antd";

const { Title } = Typography;

const Home = () => {
  const { data: products, loading } = useFetch(
    "http://localhost:5098/productos"
  );

  return (
    <div>
      <Title style={{ textAlign: "center" }}>Product List</Title>
      <ProductList products={products} loading={loading} />
    </div>
  );
};

export default Home;
