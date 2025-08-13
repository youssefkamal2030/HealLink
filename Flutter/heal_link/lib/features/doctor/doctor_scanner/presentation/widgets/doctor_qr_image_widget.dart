
import 'package:flutter/cupertino.dart';
import 'package:qr_flutter/qr_flutter.dart';

class DoctorQrImageWidget extends StatelessWidget {
  const DoctorQrImageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Center(
        child: QrImageView(
          data: '1231020',
          version: QrVersions.auto,
          size: 200.0,
        ),
      ),
    );
  }
}
