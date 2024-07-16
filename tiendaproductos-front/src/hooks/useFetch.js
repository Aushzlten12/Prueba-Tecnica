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
