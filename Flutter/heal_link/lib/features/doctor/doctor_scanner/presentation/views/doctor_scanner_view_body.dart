import 'package:flutter/material.dart';
import 'package:qr_flutter/qr_flutter.dart';

class DoctorScannerViewBody extends StatelessWidget {
  const DoctorScannerViewBody({super.key});

  @override
  Widget build(BuildContext context) {
    return  SafeArea(
      child: Column(
        children: [
          Center(
            child: QrImageView(
              data: '1231020',
              version: QrVersions.auto,
              size: 200.0,
            ),
          ),
        ],
      ),
    );
  }
}