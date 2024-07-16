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
