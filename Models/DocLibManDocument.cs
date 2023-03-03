namespace DocLibMan.Models
{
    public class DocLibManDocument
    {
        public string content { get; set; }
        public string metadata_storage_content_type { get; set; }

        public long metadata_storage_size { get; set; }

        public DateTime? metadata_storage_last_modified { get; set; }

        public string metadata_storage_name { get; set; }

        public string metadata_storage_file_extension { get; set; }

        public string metadata_content_type { get; set; }

        public string metadata_language { get; set; }

        public string metadata_title { get; set; }
        public DateTime? metadata_creation_date { get; set; }

        public string Description { get; set; }

        public string DownloadLink { get; set; }

    }
}
