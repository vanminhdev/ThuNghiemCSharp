1. Cài đặt
    - zookeeper
        - Khái niệm:
            - Zookeeper là một dịch vụ quản lý cấu hình và cung cấp dịch vụ đồng bộ hóa phân tán, được phát triển bởi Apache. Nó được thiết kế để quản lý các siêu dữ liệu và các thông tin cấu hình khác nhau của các hệ thống phân tán
            - Zookeeper giúp đảm bảo sự nhất quán và tính khả dụng của dữ liệu trong các hệ thống phân tán bằng cách cung cấp các cơ chế đồng bộ hóa và thông báo thay đổi trạng thái
            - Các tính năng chính của Zookeeper bao gồm:
                - **Quản lý cấu hình**: Lưu trữ và cung cấp quyền truy cập vào các thông tin cấu hình của các ứng dụng.
                - **Đồng bộ hóa phân tán**: Đảm bảo rằng các ứng dụng phân tán có thể hoạt động một cách đồng bộ.
                - **Quản lý phiên**: Theo dõi trạng thái phiên làm việc của các máy khách.
                - **Cung cấp khóa phân tán**: Quản lý các khóa phân tán để tránh xung đột khi nhiều máy khách truy cập vào tài nguyên chung.
    - kafka
        - Khái niệm:
            - Kafka là một nền tảng truyền tải dữ liệu theo kiểu luồng (streaming platform) được phát triển bởi Apache
            - Kafka ban đầu được LinkedIn phát triển và sau đó được tặng cho Apache Foundation
            - Kafka được thiết kế để xử lý các luồng dữ liệu lớn theo thời gian thực, với độ tin cậy và khả năng mở rộng cao
            - Các tính năng chính của Kafka bao gồm:
                - **Nhà xuất bản/Người tiêu dùng (Producer/Consumer)**: Cho phép các ứng dụng xuất bản và tiêu thụ các thông điệp thông qua các chủ đề (topics).
                - **Chủ đề (Topic)**: Là nơi lưu trữ các luồng dữ liệu, các thông điệp được phân loại vào các chủ đề.
                - **Phân vùng (Partition)**: Mỗi chủ đề có thể được chia thành nhiều phân vùng để tăng khả năng mở rộng và hiệu suất.
                - **Brokers**: Là các máy chủ trong cụm Kafka, chịu trách nhiệm lưu trữ các dữ liệu và xử lý các yêu cầu từ nhà xuất bản và người tiêu dùng.
                - **Khả năng phục hồi và độ tin cậy cao**: Kafka sử dụng cơ chế sao lưu và đồng bộ dữ liệu giữa các brokers để đảm bảo tính toàn vẹn và khả năng phục hồi của dữ liệu
        - Khởi chạy 
            - Chạy thông qua docker compose
            - Cài đặt công cụ [Kafka Tool](https://www.kafkatool.com/download.html)
                - Kết nối đến Kafka
                    - Nhập thông tin kết nối như sau:
                        - Name: Tên kết nối (tuỳ ý)
                        - Bootstrap servers: localhost:9092
                        - Zookeeper: host:localhost, port: 2181
                    - Ping thử để kiểm tra
                    - Browser: Clusters
2. Các khái niệm
    1. Kafka
        - Broker
        - Topic
        - Consumer
        